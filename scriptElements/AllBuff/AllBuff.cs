using System.Collections;
using System.Collections.Generic;
using EElements;

namespace EAllbuff
{
    [System.Serializable]
    internal abstract class AllBuff : Elements
    {
        //테크리스트, 턴제버프
        List<int> techList = new List<int>();
        //턴제버프 : ???
        List<int> turnBuffList; //key, value : buff를주는 이름, 효과에 대한 값
        
        //효과적용
        internal abstract void EffectApply();
    }

    class BTech{

    }
    class BLeader{
        
    }
    class BInfluence{

    }
}
