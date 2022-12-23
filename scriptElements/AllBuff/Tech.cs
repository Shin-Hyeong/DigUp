using UnityEngine.UI;
using EAllbuff;

namespace ETech
{
    internal class Tech : AllBuff
    {
        int point;
        int lv;
        Button button;

        string[] special = new string[4];

        void Print() { }

        internal override void EffectApply()
        {
            
        }

        internal override void fSave()
        {
            throw new System.NotImplementedException();
        }

        internal override void fLoad()
        {
            throw new System.NotImplementedException();
        }
    }
}
