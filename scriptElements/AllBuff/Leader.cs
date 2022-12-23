using EAllbuff;
using System.Collections.Generic;

namespace EElements
{
    using ELeader;
    internal partial class DataBases
    {
        //여기에 세력이 사용하는 데이터를 정의 정의된 속성은 DataBases에 들어감
        static internal List<Leader> leaderData = new List<Leader>();
        static internal int leaderLength = 0;
        //static private string leaderJsonData;
    }
}
//지도자
namespace ELeader
{

    class Leader : AllBuff
    {
        Leader() { this.elementsMode = Emode.지도자; }

        internal override void EffectApply()
        {
            
        }

        internal override void fLoad()
        {
            throw new System.NotImplementedException();
        }

        internal override void fSave()
        {
            throw new System.NotImplementedException();
        }
    }
}
