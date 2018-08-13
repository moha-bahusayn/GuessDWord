using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GuessDWordWeb.App_Code
{
    public class PlayerModel
    {

            private string name;
            private int id;
            private int errorCounter;
            private int plays;

            public string Name { get; set; }
            public int ID { get; set; }
            public int ErrorCounter { get; set; }
            public int Plays { get; set; }

    }
}