﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace CavesEtVarans {
    [TestClass]
    public class TestSKill {

        private Skill skill;
        [TestInitialize]
        public void SetUp() {
            TargetPicker picker1 = new TargetPicker(1, "target1");
            TargetPicker picker2 = new TargetPicker(1, "target2");


        }

        [TestCleanup]
        public void TearDown() {

        }
    }
}
