using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PX.Data;
using PX.Objects.SO;

namespace Hackathon.Eta.JsonGenerator
{
    public class SOOrderEntry_Extension : PXGraphExtension<SOOrderEntry>
    {
        #region testJson
        private static string testJson = @"{
            structure: true,
            content: [{
                name: ""OrderType"",
                value: ""SO""
            },
            {
                name: ""Sum"",
                value: 335.54
            },
            {
                name: ""Inner"",
                value:
                    {
                        structure: true,
                        content:
                            [{
                                name: ""key"",
                                value: ""value""
                            }]
                    }
            },
            {
                name: ""Chain"",
                value:[
                    3,
                    ""some string"",
                    {
                        bound: true,
                        view: ""CurrentDocument"",
                        field: ""OrderNbr""
                    }
                ]
            }]
        } ";

        //,
        //            {
        //name: ""Description"",
        //                value:
        //    {
        //    bound: true,
        //                    view: ""SOOrder"",
        //                    field: ""Description""
        //                }
        //},
        //            {
        //name: ""Details"",
        //                value:
        //    {
        //    repeater: true,
        //                    view: ""SOOrderDetails"",
        //                    value:
        //        {
        //        structure: true,
        //                        content:
        //            [{
        //            name: ""DetailID"",
        //                            value:
        //                {
        //                bound: true,
        //                                field: ""DetailID""
        //                            }
        //            },
        //                        {
        //            name: ""Description"",
        //                            value:
        //                {
        //                bound: true,
        //                                field: ""DetailDescription""
        //                            }
        //            }
        //                        ]
        //                    }
        //    }
        //}
        //            ]
        //        } ";
        #endregion

        public PXAction<SOOrder> hackMe;
        
        [PXButton]
        [PXUIField(DisplayName = "Hack Me")]
        public virtual IEnumerable HackMe(PXAdapter adapter)
        {
            //SOOrderEntry soGraph = PXGraph.CreateInstance<SOOrderEntry>();
     
            string jsonOutput = JsonGenerator.Generate(testJson, Base);

            return adapter.Get();
        }
    }
}
