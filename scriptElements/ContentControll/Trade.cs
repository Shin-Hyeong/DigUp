using System.Collections.Generic;
using UnityEngine;
using EContentControll;
using ETarget;

namespace ETrade
{
    class Trade : ContentControll
    {
        int appearl; //매력도
        /*관세, 보내는 받는*/
        Target target; //static 배열로 저장

        Dictionary<string, int> send;
        Dictionary<string, int> receive;

        float KwanSeChoice(){
            float _f = 0.0f;
            return _f;
        }

        internal override void Printing()
        {
            

        }

        public void Sending(Dictionary<string, int> _s){}
        public void Receiving(Dictionary<string, int> _r){}

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
