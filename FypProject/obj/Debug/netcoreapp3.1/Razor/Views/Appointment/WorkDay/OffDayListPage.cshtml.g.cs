#pragma checksum "C:\Users\chent\Documents\FypProgrammingFolder\FypProjLatest\FypProject\FypProject\Views\Appointment\WorkDay\OffDayListPage.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "62e43693bb2850c3b05790d11cac2bc0c12fe4da"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Appointment_WorkDay_OffDayListPage), @"mvc.1.0.view", @"/Views/Appointment/WorkDay/OffDayListPage.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "C:\Users\chent\Documents\FypProgrammingFolder\FypProjLatest\FypProject\FypProject\Views\_ViewImports.cshtml"
using FypProject;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\chent\Documents\FypProgrammingFolder\FypProjLatest\FypProject\FypProject\Views\_ViewImports.cshtml"
using FypProject.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\chent\Documents\FypProgrammingFolder\FypProjLatest\FypProject\FypProject\Views\_ViewImports.cshtml"
using FypProject.ViewModel;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Users\chent\Documents\FypProgrammingFolder\FypProjLatest\FypProject\FypProject\Views\_ViewImports.cshtml"
using FypProject.Config;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "C:\Users\chent\Documents\FypProgrammingFolder\FypProjLatest\FypProject\FypProject\Views\_ViewImports.cshtml"
using System.Security.Claims;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"62e43693bb2850c3b05790d11cac2bc0c12fe4da", @"/Views/Appointment/WorkDay/OffDayListPage.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"b7038a2c2513e38ede4563778815bd8dc984e076", @"/Views/_ViewImports.cshtml")]
    public class Views_Appointment_WorkDay_OffDayListPage : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<AppointmentScheduleViewModel>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "C:\Users\chent\Documents\FypProgrammingFolder\FypProjLatest\FypProject\FypProject\Views\Appointment\WorkDay\OffDayListPage.cshtml"
  
    AppointmentScheduleViewModel viewModel = Model;

#line default
#line hidden
#nullable disable
            WriteLiteral(@"<div class=""text-center mt-3"" id=""offDay-table"">
    <div class=""table-responsive  p-3"">
        <table class=""table table-bordered"" id=""user-list-table"">
            <thead>
                <tr>
                    <th>ID</th>
                    <th>Day</th>
                    <th>Operation</th>
                    <th class=""hide-btn"" id=""checkbox-action"">Action</th>
                </tr>
            </thead>
");
#nullable restore
#line 17 "C:\Users\chent\Documents\FypProgrammingFolder\FypProjLatest\FypProject\FypProject\Views\Appointment\WorkDay\OffDayListPage.cshtml"
             if (viewModel.offDayList.Count > 0)
            {
                

#line default
#line hidden
#nullable disable
#nullable restore
#line 19 "C:\Users\chent\Documents\FypProgrammingFolder\FypProjLatest\FypProject\FypProject\Views\Appointment\WorkDay\OffDayListPage.cshtml"
                 for (var i = 0; i < viewModel.offDayList.Count(); i++)
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <tbody id=\"offDayTR\">\r\n                        <tr>\r\n                            <td id=\"dayID\">");
#nullable restore
#line 23 "C:\Users\chent\Documents\FypProgrammingFolder\FypProjLatest\FypProject\FypProject\Views\Appointment\WorkDay\OffDayListPage.cshtml"
                                      Write(viewModel.offDayList[i].Id);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                            <td>");
#nullable restore
#line 24 "C:\Users\chent\Documents\FypProgrammingFolder\FypProjLatest\FypProject\FypProject\Views\Appointment\WorkDay\OffDayListPage.cshtml"
                           Write(viewModel.offDayList[i].Day);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n");
#nullable restore
#line 25 "C:\Users\chent\Documents\FypProgrammingFolder\FypProjLatest\FypProject\FypProject\Views\Appointment\WorkDay\OffDayListPage.cshtml"
                             if (!viewModel.offDayList[i].isOffDay)
                            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                <td>Open</td>\r\n");
#nullable restore
#line 28 "C:\Users\chent\Documents\FypProgrammingFolder\FypProjLatest\FypProject\FypProject\Views\Appointment\WorkDay\OffDayListPage.cshtml"
                            }
                            else
                            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                <td>Close</td>\r\n");
#nullable restore
#line 32 "C:\Users\chent\Documents\FypProgrammingFolder\FypProjLatest\FypProject\FypProject\Views\Appointment\WorkDay\OffDayListPage.cshtml"
                            }

#line default
#line hidden
#nullable disable
#nullable restore
#line 33 "C:\Users\chent\Documents\FypProgrammingFolder\FypProjLatest\FypProject\FypProject\Views\Appointment\WorkDay\OffDayListPage.cshtml"
                             if (!viewModel.offDayList[i].isOffDay)
                            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                <td id=\"checkbox-child\" class=\"hide-btn\"><input name=\"isChecked\" type=\"checkbox\"");
            BeginWriteAttribute("value", " value=\"", 1435, "\"", 1470, 1);
#nullable restore
#line 35 "C:\Users\chent\Documents\FypProgrammingFolder\FypProjLatest\FypProject\FypProject\Views\Appointment\WorkDay\OffDayListPage.cshtml"
WriteAttributeValue("", 1443, viewModel.offDayList[i].Id, 1443, 27, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" checked=\"true\" /></td>\r\n");
#nullable restore
#line 36 "C:\Users\chent\Documents\FypProgrammingFolder\FypProjLatest\FypProject\FypProject\Views\Appointment\WorkDay\OffDayListPage.cshtml"
                            }
                            else
                            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                <td id=\"checkbox-child\" class=\"hide-btn\"><input name=\"isChecked\" type=\"checkbox\"");
            BeginWriteAttribute("value", " value=\"", 1704, "\"", 1739, 1);
#nullable restore
#line 39 "C:\Users\chent\Documents\FypProgrammingFolder\FypProjLatest\FypProject\FypProject\Views\Appointment\WorkDay\OffDayListPage.cshtml"
WriteAttributeValue("", 1712, viewModel.offDayList[i].Id, 1712, 27, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" /></td>\r\n");
#nullable restore
#line 40 "C:\Users\chent\Documents\FypProgrammingFolder\FypProjLatest\FypProject\FypProject\Views\Appointment\WorkDay\OffDayListPage.cshtml"

                            }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                        </tr>\r\n                    </tbody>\r\n");
#nullable restore
#line 45 "C:\Users\chent\Documents\FypProgrammingFolder\FypProjLatest\FypProject\FypProject\Views\Appointment\WorkDay\OffDayListPage.cshtml"
                }

#line default
#line hidden
#nullable disable
#nullable restore
#line 45 "C:\Users\chent\Documents\FypProgrammingFolder\FypProjLatest\FypProject\FypProject\Views\Appointment\WorkDay\OffDayListPage.cshtml"
                 
            }

#line default
#line hidden
#nullable disable
            WriteLiteral("        </table>\r\n    </div>\r\n");
#nullable restore
#line 49 "C:\Users\chent\Documents\FypProgrammingFolder\FypProjLatest\FypProject\FypProject\Views\Appointment\WorkDay\OffDayListPage.cshtml"
     if (viewModel.offDayList.Count == 0)
    {

#line default
#line hidden
#nullable disable
            WriteLiteral("        <p>Day not Found.</p>\r\n");
#nullable restore
#line 52 "C:\Users\chent\Documents\FypProgrammingFolder\FypProjLatest\FypProject\FypProject\Views\Appointment\WorkDay\OffDayListPage.cshtml"
    }

#line default
#line hidden
#nullable disable
            WriteLiteral("</div>");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<AppointmentScheduleViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
