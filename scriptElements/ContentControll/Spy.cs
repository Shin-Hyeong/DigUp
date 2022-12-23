using System.Collections.Generic;
using UnityEngine;

using EContentControll;
using ESlot;
using ETarget;

namespace ESpy
{
    class Spy : ContentControll
    {
        List<Slot> slot;
        Target target;

        enum mode { import, export };
        mode trade = mode.import;

        internal override void Printing(){}

        void ChoiceMode() { }
        void EnableEffect() { }

        internal override void fSave ()
        {
            throw new System.NotImplementedException();
        }

        internal override void fLoad()
        {
            throw new System.NotImplementedException();
        }
    }
}
