#pragma checksum "C:\Users\chent\Documents\FypProgrammingFolder\FypProject2\FypProject\FypProject\Views\Appointment\AppointmentScheduleIndex.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "6118ceaef075c494a71ce94c7256a0fde4fc266c"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Appointment_AppointmentScheduleIndex), @"mvc.1.0.view", @"/Views/Appointment/AppointmentScheduleIndex.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"6118ceaef075c494a71ce94c7256a0fde4fc266c", @"/Views/Appointment/AppointmentScheduleIndex.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"b7038a2c2513e38ede4563778815bd8dc984e076", @"/Views/_ViewImports.cshtml")]
    public class Views_Appointment_AppointmentScheduleIndex : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<AppointmentScheduleViewModel>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("type", new global::Microsoft.AspNetCore.Html.HtmlString("text/javascript"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/js/User/User.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/js/Appointment/SpecialHoliday.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/js/Appointment/TimeSlot.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/js/Appointment/SlotDuration.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "C:\Users\chent\Documents\FypProgrammingFolder\FypProject2\FypProject\FypProject\Views\Appointment\AppointmentScheduleIndex.cshtml"
  
    AppointmentScheduleViewModel viewModel = Model;

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<div class=\"mb-5 container-fluid\">\r\n    ");
#nullable restore
#line 7 "C:\Users\chent\Documents\FypProgrammingFolder\FypProject2\FypProject\FypProject\Views\Appointment\AppointmentScheduleIndex.cshtml"
Write(await Html.PartialAsync(SystemData.ViewPagePath.WorkDayView, viewModel));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n</div>\r\n\r\n<div style=\"background: #f1f1f1; height:8px;\"></div>\r\n\r\n<div class=\"mb-5 container-fluid\">\r\n    ");
#nullable restore
#line 13 "C:\Users\chent\Documents\FypProgrammingFolder\FypProject2\FypProject\FypProject\Views\Appointment\AppointmentScheduleIndex.cshtml"
Write(await Html.PartialAsync(SystemData.ViewPagePath.HolidayView, viewModel));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n</div>\r\n\r\n<div style=\"background: #f1f1f1; height:8px;\"></div>\r\n<div class=\"mb-5 container-fluid\">\r\n    <div class=\"row\">\r\n        ");
#nullable restore
#line 19 "C:\Users\chent\Documents\FypProgrammingFolder\FypProject2\FypProject\FypProject\Views\Appointment\AppointmentScheduleIndex.cshtml"
   Write(await Html.PartialAsync(SystemData.ViewPagePath.TimeSlotView, viewModel));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        ");
#nullable restore
#line 20 "C:\Users\chent\Documents\FypProgrammingFolder\FypProject2\FypProject\FypProject\Views\Appointment\AppointmentScheduleIndex.cshtml"
   Write(await Html.PartialAsync(SystemData.ViewPagePath.SlotDurationView, viewModel));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n    </div>\r\n</div>\r\n\r\n");
            DefineSection("Scripts", async() => {
                WriteLiteral("\r\n    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "6118ceaef075c494a71ce94c7256a0fde4fc266c7808", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "6118ceaef075c494a71ce94c7256a0fde4fc266c8994", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "6118ceaef075c494a71ce94c7256a0fde4fc266c10180", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "6118ceaef075c494a71ce94c7256a0fde4fc266c11367", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_4);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral(@"

    <script type=""text/javascript"">
        var offDayTR = $('#offDayTR');
        var checkboxChild = $('#checkbox-child');
        var alertMessage = $('#alert-message');
        var closeBtn = $('#close-alert');
        var offDayTableClone;
        var isChecked = new Array();




        editBtn.click(function (e) {
            offDayTableClone = $('#offDay-table').clone();
            /*$('#offDayTR > tr > td > input[type=checkbox]:checked').each(function (index) {
                console.log(this.value);
            });*/
            isAble = true;
            console.log(""edit button clicked"");
            e.preventDefault();
            if (isAble == true) {
                $('#checkbox-action').removeClass(""hide-btn"");
                $('#offDayTR > tr').each(function (index, tr) {
                    $(this).find('#checkbox-child').removeClass(""hide-btn"");
                });
            }
        });

        cancelBtn.click(function (e) {
            e.preventDefau");
                WriteLiteral(@"lt();
            if (isAble == true) {
                isAble = false;
                btnVisibility();
                $('#offDay-table').replaceWith(offDayTableClone);
                $('#checkbox-action').addClass(""hide-btn"");
                $('#offDayTR > tr').each(function (index, tr) {
                    $(this).find('#checkbox-child').addClass(""hide-btn"");
                });
            }
        });


        saveBtn.click(function (e) {
            e.preventDefault();
            isChecked.length = 0;

            $(""#offDayTR tr"").each(function () {
                $(this).find(""td > input[type=checkbox]:checked"").each(function () {
                    if (this.checked) {
                        var color = $(this).val();
                        isChecked.push(color);
                    }
                });
            });
            $.ajax({
                type: ""POST"",
                url: ""/Appointment/UpdateOffDay"",
                data: { ""isChecked"": isCheck");
                WriteLiteral(@"ed },
                async: false,
                success: function (response) {
                    if (isAble == true) {
                        isAble = false;
                        btnVisibility();
                    }
                    $('#offDay-table').replaceWith(response);
                    $('#app-text-1').remove();
                    alertMessage.append(`<span id=""app-text-1"">Weekly operation schedule updated successfully.</span>`);
                    alertMessage.addClass(""alert-success"").removeClass(""hide-btn"");
                },
                error: function (err) {
                    $('#app-text-1').remove();
                    alertMessage.append(`<span id=""app-text-1"">uknown error occurred.</span>`);
                    alertMessage.addClass(""alert-danger"").removeClass(""hide-btn"");
                }

            });
        });

    </script>
");
            }
            );
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
