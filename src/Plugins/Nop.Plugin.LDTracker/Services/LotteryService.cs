using System;
using System.Linq;
using System.Data.Entity;
using System.Collections.Generic;
using System.Net;
using System.IO;
using System.Web.Configuration;
using HtmlAgilityPack;
using Nop.Plugin.LDTracker.Domain;
using Nop.Core.Data;
using Nop.Services.Events;
using Nop.Plugin.LDTracker.Extensions;

namespace Nop.Plugin.LDTracker.Services
{
    public class LotteryService : ILotteryService
    {
        private readonly IRepository<Lottery> _lottery;
        private readonly IRepository<LotteryFull> _lotteryFull;
        private readonly IEventPublisher _eventPublisher;

        public LotteryService(IRepository<Lottery> lottery, IRepository<LotteryFull> lotteryFull, IEventPublisher eventPublisher)
        {
            _lottery = lottery;
            _lotteryFull = lotteryFull;
            _eventPublisher = eventPublisher;
        }

        public virtual Lottery GetLotteryByDate(string date)
        {
            if (string.IsNullOrEmpty(date))
                throw new ArgumentNullException("Lottery");

            var lottery = _lottery.Table.FirstOrDefault(o => o.LotteryDate == date);

            // Nếu chưa được lưu trên database thì tự động lấy kết quả trên web
            if(lottery == null)
            {
                LotteryFullService service = new LotteryFullService(_lotteryFull, _lottery, _eventPublisher);
                var lotteryFull = service.GetLotteryBy24H(date);
                if (lotteryFull == null)
                    lotteryFull = service.GetLotteryByKetQuaOrg(date);
                if (lotteryFull == null) return null;
                else
                    lottery = _lottery.Table.FirstOrDefault(o => o.LotteryDate == date);
            }
            return lottery;
        }

        public virtual void InsertLottery(List<string> result, string date)
        {
            if(result == null)
                throw new ArgumentNullException("Lottery");

            var lottery = new Lottery {
                item1 = result[0],
                item2 = result[1],
                item3 = result[2],
                item4 = result[3],
                item5 = result[4],
                item6 = result[5],
                item7 = result[6],
                item8 = result[7],
                item9 = result[8],
                item10 = result[9],
                item11 = result[10],
                item12 = result[11],
                item13 = result[12],
                item14 = result[13],
                item15 = result[14],
                item16 = result[15],
                item17 = result[16],
                item18 = result[17],
                item19 = result[18],
                item20 = result[19],
                item21 = result[20],
                item22 = result[21],
                item23 = result[22],
                item24 = result[23],
                item25 = result[24],
                item26 = result[25],
                item27 = result[26],
                LotteryDate = date,
                LotteryType = (int) ConstLib.LotteryType.KetQuaTruyenThong                
            };
            // Nếu đã có kết quả thì xóa kết quả cũ , insert kết quả mới
            var lotteryOld = _lottery.Table.FirstOrDefault(o => o.LotteryDate == date);
            if (lotteryOld != null)
            {
                lotteryOld = lottery.MapTo(lotteryOld);
                _lottery.Update(lotteryOld);
            }
            else
                _lottery.Insert(lottery);

            //event notification
            _eventPublisher.EntityInserted(lottery);
        }

        public virtual void UpdateLottery(Lottery lottery)
        {
            if (lottery == null || lottery.Id == 0)
                throw new ArgumentNullException("Lottery");

            _lottery.Update(lottery);

            //event notification
            _eventPublisher.EntityUpdated(lottery);
        }

        public virtual void DeleteLottery(int lotteryId)
        {
            if (lotteryId == 0)
                throw new ArgumentNullException("Lottery");

            var lottery = _lottery.GetById(lotteryId);
            _lottery.Delete(lottery);

            //event notification
            _eventPublisher.EntityDeleted(lottery);
        }
    }

    public class LotteryFullService : ILotteryFullService
    {
        private readonly IRepository<LotteryFull> _lotteryFull;
        private readonly IRepository<Lottery> _lottery;
        private readonly IEventPublisher _eventPublisher;

        public LotteryFullService(IRepository<LotteryFull> lotteryFull, IRepository<Lottery> lottery, IEventPublisher eventPublisher)
        {
            _lotteryFull = lotteryFull;
            _lottery = lottery;
            _eventPublisher = eventPublisher;
        }

        public virtual LotteryFull GetLotteryByDate(string date)
        {
            if (string.IsNullOrEmpty(date))
                throw new ArgumentNullException("LotteryFull");

            var lottery = _lotteryFull.Table.FirstOrDefault(o => o.LotteryDate == date);

            // Nếu chưa được lưu trên database thì tự động lấy kết quả trên web
            if (lottery == null)
            {
                lottery = GetLotteryBy24H(date);
                if (lottery == null)
                    lottery = GetLotteryByKetQuaOrg(date);
            }
            return lottery;
        }

        public virtual LotteryFull GetLotteryBy24H(string date)
        {
            List<string> result = new List<string>();
            List<string> resultItem = new List<string>();
            try
            {
                string url = WebConfigurationManager.AppSettings["ketquaxoso.24h.com.vn"];
                if (string.IsNullOrEmpty(url))
                    throw new Exception("Không tìm thấy đường dẫn đến trang ketquaxoso.24h.com.vn");

                url = string.Format(url, date.Replace("/", "-"));

                WebRequest myWebRequest = WebRequest.Create(url);
                WebResponse myWebResponse = myWebRequest.GetResponse();//Returns a response from an Internet resource
                Stream streamResponse = myWebResponse.GetResponseStream();//return the data stream from the internet and save it in the stream
                StreamReader sreader = new StreamReader(streamResponse);//reads the data stream
                string html = sreader.ReadToEnd();//reads it to the end

                HtmlDocument doc = new HtmlDocument();
                doc.LoadHtml(html);
                HtmlNodeCollection nodes = doc.DocumentNode.SelectNodes("//td[@class='number_kq']");
                int count = 0;
                if (nodes != null)
                {
                    foreach (HtmlNode node in nodes)
                    {
                        if (count == 8)
                            break;
                        if (node.InnerText.Contains("-"))
                        {
                            string[] words = node.InnerText.Split('-');
                            foreach (string word in words)
                            {
                                result.Add(word);
                                resultItem.Add(word.Substring(word.Length - 2));
                            }
                        }
                        else
                        {
                            result.Add(node.InnerText);
                            resultItem.Add(node.InnerText.Substring(node.InnerText.Length - 2));
                        }
                        count++;
                    }

                    sreader.Close();
                    myWebResponse.Close();

                    // Lưu kết quả vào database
                    var lottery = InsertLottery(result, date);

                    // Lưu kết quả sau khi cắt
                    ILotteryService service = new LotteryService(_lottery, _lotteryFull, _eventPublisher);
                    service.InsertLottery(resultItem, date);
                    return lottery;
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public virtual LotteryFull GetLotteryByKetQuaOrg(string date)
        {
            List<string> result = new List<string>();
            List<string> resultItem = new List<string>();
            try
            {
                string url = WebConfigurationManager.AppSettings["ketqua.org"];
                if (string.IsNullOrEmpty(url))
                    throw new Exception("Không tìm thấy đường dẫn đến trang ketqua.net");

                url = string.Format(url, date);

                WebRequest myWebRequest = WebRequest.Create(url);
                WebResponse myWebResponse = myWebRequest.GetResponse();
                Stream streamResponse = myWebResponse.GetResponseStream();

                StreamReader sreader = new StreamReader(streamResponse);
                string html = sreader.ReadToEnd();
                HtmlDocument doc = new HtmlDocument();
                doc.LoadHtml(html);

                // Giải đặc biệt + Giải nhất
                HtmlNodeCollection node1 = doc.DocumentNode.SelectNodes("//div[@class='box_center']/table[@class='tbl_home_result']/tr/td[@align='center']");

                if (node1 != null)
                {
                    foreach (HtmlNode node in node1)
                    {
                        result.Add(node.InnerText);
                        resultItem.Add(node.InnerText.Substring(node.InnerText.Length - 2));
                    }

                    HtmlNodeCollection nodes = doc.DocumentNode.SelectNodes("//div[@class='box_center']/table[@class='tbl_home_result']/tr/td/table[@class='tbl_home_result_sub']/tr/td");
                    foreach (HtmlNode node in nodes)
                    {
                        result.Add(node.InnerText);
                        resultItem.Add(node.InnerText.Substring(node.InnerText.Length - 2));
                    }

                    sreader.Close();
                    myWebResponse.Close();

                    // Lưu kết quả LotteryFull vào database
                    var lottery = InsertLottery(result, date);

                    // Lưu kết quả sau khi cắt
                    ILotteryService service = new LotteryService(_lottery, _lotteryFull, _eventPublisher);
                    service.InsertLottery(resultItem, date);

                    return lottery;
                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public virtual LotteryFull InsertLottery(List<string> result, string date)
        {
            if (result == null)
                throw new ArgumentNullException("Lottery");

            var lottery = new LotteryFull
            {
                item1 = result[0],
                item2 = result[1],
                item3 = result[2],
                item4 = result[3],
                item5 = result[4],
                item6 = result[5],
                item7 = result[6],
                item8 = result[7],
                item9 = result[8],
                item10 = result[9],
                item11 = result[10],
                item12 = result[11],
                item13 = result[12],
                item14 = result[13],
                item15 = result[14],
                item16 = result[15],
                item17 = result[16],
                item18 = result[17],
                item19 = result[18],
                item20 = result[19],
                item21 = result[20],
                item22 = result[21],
                item23 = result[22],
                item24 = result[23],
                item25 = result[24],
                item26 = result[25],
                item27 = result[26],
                LotteryDate = date,
                LotteryType = (int)ConstLib.LotteryType.KetQuaTruyenThong
            };

            // Nếu đã có kết quả thì update
            var lotteryOld = _lotteryFull.Table.FirstOrDefault(o => o.LotteryDate == date);
            if (lotteryOld != null)
            {
                lotteryOld = lottery.MapTo(lotteryOld);
                _lotteryFull.Update(lotteryOld);

            }else
                _lotteryFull.Insert(lottery);

            //event notification
            _eventPublisher.EntityInserted(lottery);

            return lottery;
        }

        public virtual void UpdateLottery(LotteryFull lottery)
        {
            if (lottery == null || lottery.Id == 0)
                throw new ArgumentNullException("Lottery");

            _lotteryFull.Update(lottery);

            //event notification
            _eventPublisher.EntityUpdated(lottery);
        }

        public virtual void DeleteLottery(int lotteryId)
        {
            if (lotteryId == 0)
                throw new ArgumentNullException("LotteryFull");

            var lottery = _lotteryFull.GetById(lotteryId);
            _lotteryFull.Delete(lottery);

            //event notification
            _eventPublisher.EntityDeleted(lottery);
        }
    }
}
