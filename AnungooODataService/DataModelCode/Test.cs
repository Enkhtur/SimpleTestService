using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using System.ComponentModel;
namespace AnungooODataService.Anungoo
{

    public partial class Test
    {
        public Test(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
    }

}
