using UnityEngine.UI;
using EElements;
using EGeneration;

namespace EContentControll
{
    internal abstract class ContentControll : Elements
    {
        Button button;
        int yourID; //소유 ID

        public ContentControll(){
            yourID = ID; //소유 아이디
        }

        internal abstract void Printing();
    }
}
