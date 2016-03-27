using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Plugin.LDTracker.Domain;
using Nop.Plugin.LDTracker.Models;

namespace Nop.Plugin.LDTracker.Services
{
    public interface ILotteryService
    {
        Lottery GetLotteryByDate(string date);

        void InsertLottery(List<string> lottery, string date);

        void DeleteLottery(int lotteryId);

        void UpdateLottery(Lottery lottery);
    }

    public interface ILotteryFullService
    {
        LotteryFull GetLotteryByDate(string date);

        LotteryFull InsertLottery(List<string> lottery, string date);

        void DeleteLottery(int lotteryId);

        void UpdateLottery(LotteryFull lottery);

        LotteryFull GetLotteryBy24H(string date);

        LotteryFull GetLotteryByKetQuaOrg(string date);
    }
}
