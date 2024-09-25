using QFramework;
using System;
using SaveData;
using UnityEngine;

namespace Models
{
    class TimeModel : AbstractModel
    {
        public DateTime _lastExitTime { get; private set; }
        protected override void OnInit()
        {
            Load();
            CommonMono.AddQuitAction(Save);
        }

        void Save()
        {
            TimeSaveData timeSaveData = new TimeSaveData();
            timeSaveData.lastExitTime = DateTime.Now;
            this.GetUtility<Storage>().Save(timeSaveData);
        }

        void Load()
        {
            var data = this.GetUtility<Storage>().Load<TimeSaveData>();
            if (data == default)
                return;
            _lastExitTime = data.lastExitTime;
        }
    }
}
