#pragma checksum "C:\Users\chent\Documents\FypProgrammingFolder\FypProject2\FypProject\FypProject\Views\Appointment\ApptList\ApptListPage.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "112904223dd77eb876ee9f596fc5915bf87d5f3e"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Appointment_ApptList_ApptListPage), @"mvc.1.0.view", @"/Views/Appointment/ApptList/ApptListPage.cshtml")]
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
#line 1 "C:\Users\chent\Documents\FypProgrammingFolder\FypProject2\FypProject\FypProject\Views\_ViewImports.cshtml"
using FypProject;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\chent\Documents\FypProgrammingFolder\FypProject2\FypProject\FypProject\Views\_ViewImports.cshtml"
using FypProject.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\chent\Documents\FypProgrammingFolder\FypProject2\FypProject\FypProject\Views\_ViewImports.cshtml"
using FypProject.ViewModel;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Users\chent\Documents\FypProgrammingFolder\FypProject2\FypProject\FypProject\Views\_ViewImports.cshtml"
using FypProject.Config;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "C:\Users\chent\Documents\FypProgrammingFolder\FypProject2\FypProject\FypProject\Views\_ViewImports.cshtml"
using System.Security.Claims;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"112904223dd77eb876ee9f596fc5915bf87d5f3e", @"/Views/Appointment/ApptList/ApptListPage.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"b7038a2c2513e38ede4563778815bd8dc984e076", @"/Views/_ViewImports.cshtml")]
    public class Views_Appointment_ApptList_ApptListPage : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral(@"
    <nav>
        <div class=""nav nav-tabs mt-5 justify-content-end"" id=""nav-tab"" role=""tablist"">
            <a class=""nav-item nav-link active"" data-toggle=""tab"" href=""#nav-upcoming"" role=""tab"" aria-controls=""nav-upcoming"" aria-selected=""true"">Upcoming</a>
            <a class=""nav-item nav-link"" data-toggle=""tab"" role=""tab"" href=""#nav-past"" aria-controls=""nav-past"" aria-selected=""false"">Past</a>
        </div>
    </nav>
        <div class=""upcoming-table text-center hide-btn"" id=""upcoming-table"">
            <div class=""table-responsive p-3"" id=""appt-list-table-div"">
                <table class=""table table-bordered dt-responsive"" width=""100%"" id=""appt-list-table"">
                    <thead>
                        <tr>
                            <th>Full Name</th>
                            <th>NRIC</th>
                            <th>Phone Number</th>
                            <th>Date</th>
                            <th>Slot</th>
                            <th>Appointment Typ");
            WriteLiteral(@"e</th>
                            <th>Status</th>
                            <th>Action</th>
                        </tr>
                    </thead>
                </table>
            </div>
        </div>
        <div class=""past-table text-center hide-btn"" id=""past-table"">
            <div class=""table-responsive p-3"" id=""appt-list-table-div"">
                <table class=""table table-bordered dt-responsive"" width=""100%"" id=""no-show-appt-list-table"">
                    <thead>
                        <tr>
                            <th>Full Name</th>
                            <th>NRIC</th>
                            <th>Phone Number</th>
                            <th>Date</th>
                            <th>Slot</th>
                            <th>Appointment Type</th>
                            <th>Status</th>
                            <th>Action</th>
                        </tr>
                    </thead>
                </table>
            </div>
        </");
            WriteLiteral("div>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
