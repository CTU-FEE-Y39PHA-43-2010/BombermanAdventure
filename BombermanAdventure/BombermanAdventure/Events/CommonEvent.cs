using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BombermanAdventure.Models.GameModels;

namespace BombermanAdventure.Events
{
    class CommonEvent
    {
        AbstractGameModel model;

        public AbstractGameModel Model 
        {
            get { return model; }
        }

        public CommonEvent(AbstractGameModel irModel) 
        {
            model = irModel;
        }
    }
}
