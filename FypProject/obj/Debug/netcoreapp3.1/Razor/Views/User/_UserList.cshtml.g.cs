#pragma checksum "C:\Users\chent\Documents\FypProgrammingFolder\FypProjectGithub\FypProjectWeb\FypProject\Views\User\_UserList.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "cfd9b495c60578fd1fecc87a4d0ad402c09eae1c"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_User__UserList), @"mvc.1.0.view", @"/Views/User/_UserList.cshtml")]
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
#line 1 "C:\Users\chent\Documents\FypProgrammingFolder\FypProjectGithub\FypProjectWeb\FypProject\Views\_ViewImports.cshtml"
using FypProject;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\chent\Documents\FypProgrammingFolder\FypProjectGithub\FypProjectWeb\FypProject\Views\_ViewImports.cshtml"
using FypProject.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\chent\Documents\FypProgrammingFolder\FypProjectGithub\FypProjectWeb\FypProject\Views\_ViewImports.cshtml"
using FypProject.ViewModel;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Users\chent\Documents\FypProgrammingFolder\FypProjectGithub\FypProjectWeb\FypProject\Views\_ViewImports.cshtml"
using FypProject.Config;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "C:\Users\chent\Documents\FypProgrammingFolder\FypProjectGithub\FypProjectWeb\FypProject\Views\_ViewImports.cshtml"
using System.Security.Claims;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"cfd9b495c60578fd1fecc87a4d0ad402c09eae1c", @"/Views/User/_UserList.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"b7038a2c2513e38ede4563778815bd8dc984e076", @"/Views/_ViewImports.cshtml")]
    public class Views_User__UserList : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<UserViewModel>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "User", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "UserDetail", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "C:\Users\chent\Documents\FypProgrammingFolder\FypProjectGithub\FypProjectWeb\FypProject\Views\User\_UserList.cshtml"
  
    UserViewModel viewModel = Model;

    ViewData["Title"] = "Home Page";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"    <div class=""text-center"">
        <div class=""table-responsive"">
            <table class=""table table-bordered"" id=""user-list-table"">
                <thead>
                    <tr>
                        <th>Id</th>
                        <th>Full Name</th>
                        <th>NRIC</th>
                        <th>Phone Number</th>
                        <th>Action</th>
                    </tr>
                </thead>
");
#nullable restore
#line 19 "C:\Users\chent\Documents\FypProgrammingFolder\FypProjectGithub\FypProjectWeb\FypProject\Views\User\_UserList.cshtml"
                 if (viewModel.DataList.Count > 0)
                {
                    

#line default
#line hidden
#nullable disable
#nullable restore
#line 21 "C:\Users\chent\Documents\FypProgrammingFolder\FypProjectGithub\FypProjectWeb\FypProject\Views\User\_UserList.cshtml"
                     foreach (var users in viewModel.DataList)
                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                        <tbody>\r\n                            <tr>\r\n                                <td>");
#nullable restore
#line 25 "C:\Users\chent\Documents\FypProgrammingFolder\FypProjectGithub\FypProjectWeb\FypProject\Views\User\_UserList.cshtml"
                               Write(users.Id);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                                <td>");
#nullable restore
#line 26 "C:\Users\chent\Documents\FypProgrammingFolder\FypProjectGithub\FypProjectWeb\FypProject\Views\User\_UserList.cshtml"
                               Write(users.FullName);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                                <td>");
#nullable restore
#line 27 "C:\Users\chent\Documents\FypProgrammingFolder\FypProjectGithub\FypProjectWeb\FypProject\Views\User\_UserList.cshtml"
                               Write(users.NRIC);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                                <td>");
#nullable restore
#line 28 "C:\Users\chent\Documents\FypProgrammingFolder\FypProjectGithub\FypProjectWeb\FypProject\Views\User\_UserList.cshtml"
                               Write(users.PhoneNumber);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                                <td>\r\n                                    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "cfd9b495c60578fd1fecc87a4d0ad402c09eae1c7259", async() => {
                WriteLiteral("<i class=\"fa fa-info-circle fa-lg\"></i>");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 30 "C:\Users\chent\Documents\FypProgrammingFolder\FypProjectGithub\FypProjectWeb\FypProject\Views\User\_UserList.cshtml"
                                                                                       WriteLiteral(users.Id);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                                </td>\r\n                            </tr>\r\n                        </tbody>\r\n");
#nullable restore
#line 34 "C:\Users\chent\Documents\FypProgrammingFolder\FypProjectGithub\FypProjectWeb\FypProject\Views\User\_UserList.cshtml"
                    }

#line default
#line hidden
#nullable disable
#nullable restore
#line 34 "C:\Users\chent\Documents\FypProgrammingFolder\FypProjectGithub\FypProjectWeb\FypProject\Views\User\_UserList.cshtml"
                     
                }

#line default
#line hidden
#nullable disable
            WriteLiteral("            </table>         \r\n        </div>\r\n");
#nullable restore
#line 38 "C:\Users\chent\Documents\FypProgrammingFolder\FypProjectGithub\FypProjectWeb\FypProject\Views\User\_UserList.cshtml"
         if (viewModel.DataList.Count == 0)
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <p>User not Found.</p>\r\n");
#nullable restore
#line 41 "C:\Users\chent\Documents\FypProgrammingFolder\FypProjectGithub\FypProjectWeb\FypProject\Views\User\_UserList.cshtml"
            }

#line default
#line hidden
#nullable disable
            WriteLiteral("    </div>\r\n\r\n\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<UserViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
