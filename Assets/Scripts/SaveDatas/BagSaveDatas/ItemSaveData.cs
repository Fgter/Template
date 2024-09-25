using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaveData
{
    struct ItemSaveData
    {
        public int id;
        public int count;

        public ItemSaveData(int id,int count)
        {
            this.id = id;
            this.count = count;
        }
    }
}
