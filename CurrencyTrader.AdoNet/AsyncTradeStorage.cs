using CurrencyTrader.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// ===============================
// AUTHOR     : Sumathilatha Myla
// CREATE DATE     :11/09/2019
// PURPOSE     : Adding an Asynchronous Trade Storage 
// ===============================

namespace CurrencyTrader.AdoNet
{
    public class AsyncTradeStorage : ITradeStorage
    {
        private readonly ILogger logger;

        private ITradeStorage SynchTradeStorage;

        /// <summary>
        /// Add an Asynchronous decorator to the AdoNetTradeStorage
        /// </summary>
        /// <param name="logger"></param>
        public AsyncTradeStorage(ILogger logger)
        {
            this.logger = logger;
            SynchTradeStorage = new AdoNetTradeStorage(logger);
        }

        /// <summary>
        /// AdoNetTradeStorage object is called SynchTradeStorage, 
        /// this is how you call the Persist method in another task
        /// </summary>
        /// <param name="trades"></param>
        public void Persist(IEnumerable<TradeRecord> trades)
        {
            logger.LogInfo("Starting sync trade storage");
           // syncTradeStorage.Persist(trades);
            Task.Run(() => SynchTradeStorage.Persist(trades));
        }
    }
}
