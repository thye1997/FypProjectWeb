#pragma checksum "C:\Users\chent\Documents\FypProgrammingFolder\FypProjectGithub1\FypProjectWeb\FypProject\Views\User\UserDetail.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "d260663f8ef0486dbedbb84c46ac85ad0ea90bfd"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_User_UserDetail), @"mvc.1.0.view", @"/Views/User/UserDetail.cshtml")]
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
#line 1 "C:\Users\chent\Documents\FypProgrammingFolder\FypProjectGithub1\FypProjectWeb\FypProject\Views\_ViewImports.cshtml"
using FypProject;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\chent\Documents\FypProgrammingFolder\FypProjectGithub1\FypProjectWeb\FypProject\Views\_ViewImports.cshtml"
using FypProject.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\chent\Documents\FypProgrammingFolder\FypProjectGithub1\FypProjectWeb\FypProject\Views\_ViewImports.cshtml"
using FypProject.ViewModel;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Users\chent\Documents\FypProgrammingFolder\FypProjectGithub1\FypProjectWeb\FypProject\Views\_ViewImports.cshtml"
using FypProject.Config;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "C:\Users\chent\Documents\FypProgrammingFolder\FypProjectGithub1\FypProjectWeb\FypProject\Views\_ViewImports.cshtml"
using System.Security.Claims;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"d260663f8ef0486dbedbb84c46ac85ad0ea90bfd", @"/Views/User/UserDetail.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"b7038a2c2513e38ede4563778815bd8dc984e076", @"/Views/_ViewImports.cshtml")]
    public class Views_User_UserDetail : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<UserViewModel>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("value", "", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("value", "Male", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("value", "Female", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("id", new global::Microsoft.AspNetCore.Html.HtmlString("userForm"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/js/User/formvalidation.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("type", new global::Microsoft.AspNetCore.Html.HtmlString("text/javascript"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_6 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/js/User/User.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_7 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/js/Ajax/ajaxcall.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper;
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "C:\Users\chent\Documents\FypProgrammingFolder\FypProjectGithub1\FypProjectWeb\FypProject\Views\User\UserDetail.cshtml"
  
    UserViewModel userViewModel = Model;

#line default
#line hidden
#nullable disable
            WriteLiteral(@"    <div class=""container-fluid mb-5"">
            <div class=""card-header text-center font-weight-bold bg-light text-dark"" style=""margin:0 -15px;"">
                PATIENT Details
                <a class=""float-right"" id=""dropdownMenuButton"" data-toggle=""dropdown"" aria-haspopup=""true"" style=""cursor:pointer;"" aria-expanded=""false"">
                    <i class=""bi bi-three-dots-vertical""></i>
                </a>
                <div class=""dropdown-menu"" aria-labelledby=""dropdownMenuButton"">
                    <a class=""dropdown-item"" href=""#"" id=""edit-btn"">Edit</a>
                </div>
            </div>
            <div class=""alert hide-btn"" id=""alert-message"" role=""alert"" style=""margin:0 -15px;"">
                <button type=""button"" class=""close"" id=""close-alert"">
                    <span>&times;</span>
                </button>
            </div>

            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "d260663f8ef0486dbedbb84c46ac85ad0ea90bfd8151", async() => {
                WriteLiteral(@"
                <div class=""p-3"">
                    <div class=""form-row"">
                        <div class=""col"">
                            <label for=""inputEmail3"" class=""col-sm-3 col-form-label"">NRIC:</label>
                            <input type=""text"" class=""form-control"" id=""nric"" name=""nric""");
                BeginWriteAttribute("placeholder", " placeholder=\"", 1304, "\"", 1318, 0);
                EndWriteAttribute();
                BeginWriteAttribute("value", " value=\"", 1319, "\"", 1351, 1);
#nullable restore
#line 26 "C:\Users\chent\Documents\FypProgrammingFolder\FypProjectGithub1\FypProjectWeb\FypProject\Views\User\UserDetail.cshtml"
WriteAttributeValue("", 1327, userViewModel.user.NRIC, 1327, 24, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(@" disabled>
                        </div>
                        <div class=""col"">
                            <label for=""inputEmail3"" class=""col-sm-3 col-form-label"">Name:</label>
                            <input type=""text"" class=""form-control"" id=""name"" name=""fullName""");
                BeginWriteAttribute("value", " value=\"", 1632, "\"", 1668, 1);
#nullable restore
#line 30 "C:\Users\chent\Documents\FypProgrammingFolder\FypProjectGithub1\FypProjectWeb\FypProject\Views\User\UserDetail.cshtml"
WriteAttributeValue("", 1640, userViewModel.user.FullName, 1640, 28, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(@" disabled>
                        </div>
                    </div>

                    <div class=""form-row"">
                        <div class=""col"">
                            <label for=""inputEmail3"" class=""col-sm-3 col-form-label"" style=""white-space:nowrap;"">Phone Number:</label>
                            <input type=""text"" class=""form-control"" id=""phoneNumber"" name=""phoneNumber""");
                BeginWriteAttribute("value", " value=\"", 2069, "\"", 2108, 1);
#nullable restore
#line 37 "C:\Users\chent\Documents\FypProgrammingFolder\FypProjectGithub1\FypProjectWeb\FypProject\Views\User\UserDetail.cshtml"
WriteAttributeValue("", 2077, userViewModel.user.PhoneNumber, 2077, 31, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(@" disabled>
                        </div>
                        <div class=""col"">
                            <label for=""inputEmail3"" class=""col-sm-3 col-form-label"" style=""white-space:nowrap;"">Day of Birth:</label>
                            <input type=""date"" class=""form-control"" id=""DOB"" name=""DOB""");
                BeginWriteAttribute("value", " value=\"", 2419, "\"", 2450, 1);
#nullable restore
#line 41 "C:\Users\chent\Documents\FypProgrammingFolder\FypProjectGithub1\FypProjectWeb\FypProject\Views\User\UserDetail.cshtml"
WriteAttributeValue("", 2427, userViewModel.user.DOB, 2427, 23, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(@" disabled>
                        </div>
                    </div>
                    <div class=""form-row"">
                        <div class=""col-6"">

                            <label for=""genderSelect"" class=""col-sm-3 col-form-label"">Gender:</label>
                            <select class=""custom-select"" id=""genderSelect"" name=""Gender"" disabled>
                                ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "d260663f8ef0486dbedbb84c46ac85ad0ea90bfd12041", async() => {
                    WriteLiteral("Select Gender");
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_0.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n                                ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "d260663f8ef0486dbedbb84c46ac85ad0ea90bfd13304", async() => {
                    WriteLiteral("Male");
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_1.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n                                ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "d260663f8ef0486dbedbb84c46ac85ad0ea90bfd14558", async() => {
                    WriteLiteral("Female");
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_2.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n                            </select>\r\n                            <input class=\"form-control\" id=\"defaultGender\"");
                BeginWriteAttribute("value", " value=\"", 3144, "\"", 3178, 1);
#nullable restore
#line 53 "C:\Users\chent\Documents\FypProgrammingFolder\FypProjectGithub1\FypProjectWeb\FypProject\Views\User\UserDetail.cshtml"
WriteAttributeValue("", 3152, userViewModel.user.Gender, 3152, 26, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(@" hidden>
                        </div>
                    </div>
                    <span class=""d-block"" id=""genderErr"" style=""color:red;""></span>
                    <div class=""form-group row mt-2"">
                        <div class=""cancel-btn-div hide-btn"">
                            <button class=""btn btn-light"" id=""cancel-btn"">Cancel</button>
                        </div>
                        <div class=""save-btn-div hide-btn"">
                            <button type=""submit"" class=""btn btn-success"" id=""save-btn"">Save</button>
                        </div>
                    </div>
                </div>
            ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(@"
            </div>
            <!--<div class=""col-"" style=""background: #f1f1f1; width:2px;""></div>-->
            <!--<div class=""bg-white rounded col"" style=""margin-right:-30px;"">
            <div class=""card-header text-center font-weight-bold bg-light text-dark"" style=""margin:0 -15px;"">
                MEDICAL HISTORY
");
#nullable restore
#line 72 "C:\Users\chent\Documents\FypProgrammingFolder\FypProjectGithub1\FypProjectWeb\FypProject\Views\User\UserDetail.cshtml"
                 if (User.FindFirst(ClaimTypes.Role).Value == SystemData.Role.Doctor)
                {

#line default
#line hidden
#nullable disable
            WriteLiteral(@"                    <a class=""float-right"" id=""dropdownMenuButton"" data-toggle=""dropdown"" aria-haspopup=""true"" style=""cursor:pointer;"" aria-expanded=""false"">
                        <i class=""bi bi-three-dots-vertical""></i>
                    </a>
                <div class=""dropdown-menu"" aria-labelledby=""dropdownMenuButton"">
                    <a class=""dropdown-item"" href=""#"" data-toggle=""modal"" data-target="".med-history-modal"">Add</a>
                </div>
");
#nullable restore
#line 80 "C:\Users\chent\Documents\FypProgrammingFolder\FypProjectGithub1\FypProjectWeb\FypProject\Views\User\UserDetail.cshtml"
                }

#line default
#line hidden
#nullable disable
            WriteLiteral(@"            </div>
            <div class=""alert hide-btn"" id=""medHistory-alert-message"" role=""alert"" style=""margin:0 -15px;"">
                <button type=""button"" class=""close"" id=""close-alert"" onclick=""closeMessageFunc($('#medHistory-alert-message'))"">
                    <span>&times;</span>
                </button>
            </div>
            <div class=""text-center"">
                <div class=""table-responsive p-3"" id=""patientUser-list-table-div"">
                    <table class=""table table-bordered dt-responsive"" width=""100%"" id=""medHistory-list-table"">
                        <thead>
                            <tr>
                                <th>ID</th>
                                <th>Description</th>
                            </tr>
                        </thead>
                    </table>
                </div>
            </div>
        </div>-->
            <div class=""mb-5 container-fluid"">
                <div class=""card-header text-center font-weight-b");
            WriteLiteral(@"old bg-light text-dark"" style=""margin:0 -15px;"">
                    MEDICAL HISTORY
                </div>
                <div class=""text-center"">
                    <div class=""table-responsive p-3"" id=""patientUser-list-table-div"">
                        <table class=""table table-bordered dt-responsive"" width=""100%"" id=""medHistory-list-table"">
                            <thead>
                                <tr>
                                    <th>ID</th>
                                    <th>Date</th>
                                    <th>Slot</th>
                                    <th>Service</th>
                                    <th>Result</th>
                                    <th>Medical Prescription</th>
                                    <th>By Doctor</th>
                                </tr>
                            </thead>
                        </table>
                    </div>
                </div>
        </div>
        <!--<div class=""modal f");
            WriteLiteral(@"ade med-history-modal"" tabindex=""-1"" role=""dialog"" aria-labelledby=""myExtraLargeModalLabel"" aria-hidden=""true"">
        <div class=""modal-dialog modal-lg"">
            <div class=""modal-content  pt-2 pl-3 pr-3 pb-3"">
                <div class=""modal-header"">
                    <h5 class=""modal-title"">Add Medical History</h5>
                    <button type=""button"" class=""close"" data-dismiss=""modal"" aria-label=""Close"">
                        <span aria-hidden=""true"">&times;</span>
                    </button>
                </div>
                <form id=""medHistoryForm"" method=""post"">
                    <div class=""row mt-2"">
                        <div class=""form-group col"">
                            <label for=""InputMedHistory"">Description</label>
                            <textarea class=""form-control"" id=""InputMedHistory"" name=""Description"" placeholder=""Description""></textarea>
                        </div>
                        <input type=""text"" id=""med-history-userId"" v");
            WriteLiteral("alue=\"");
#nullable restore
#line 137 "C:\Users\chent\Documents\FypProgrammingFolder\FypProjectGithub1\FypProjectWeb\FypProject\Views\User\UserDetail.cshtml"
                                                                     Write(userViewModel.user.Id);

#line default
#line hidden
#nullable disable
            WriteLiteral(@""" hidden>

                    </div>

                    <span class=""text-danger d-block"" id=""genderErr"" style=""margin-top:-16px;""></span>
                    <div class=""modal-footer"">
                        <button class=""btn btn-primary"" id=""add-MedHistory-btn"">Add</button>
                    </div>
                </form>
            </div>
        </div>
    </div>-->

        <input type=""text"" id=""id""");
            BeginWriteAttribute("value", " value=\"", 8301, "\"", 8331, 1);
#nullable restore
#line 150 "C:\Users\chent\Documents\FypProgrammingFolder\FypProjectGithub1\FypProjectWeb\FypProject\Views\User\UserDetail.cshtml"
WriteAttributeValue("", 8309, userViewModel.user.Id, 8309, 22, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" hidden>\r\n\r\n");
            DefineSection("Scripts", async() => {
                WriteLiteral("\r\n            ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "d260663f8ef0486dbedbb84c46ac85ad0ea90bfd24076", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_4);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n            ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "d260663f8ef0486dbedbb84c46ac85ad0ea90bfd25184", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_5);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_6);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n            ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "d260663f8ef0486dbedbb84c46ac85ad0ea90bfd26379", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_5);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_7);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral(@"
            <script type=""text/javascript"">

                var alertMessage = $('#alert-message');
                var medHisAlertMsg = $('#medHistory-alert-message');
                var closeBtn = $('#close-alert');
                var name = $('#name').val();
                var nric = $('#nric').val();
                var phoneNum = $('#phoneNumber').val();
                var userId = $('#id').val();
                var DOB = $('#DOB').val();
                var res = { response: null };
                var table;
                var defaultGender = $('#defaultGender').val();
                $(document).ready(function () {
                    console.log(""default gender=>"" + defaultGender);
                    $('#genderSelect option[value=' + defaultGender + ']').attr('selected', 'selected');
                });

                window.onload = function () {
                    document.getElementById('userForm').reset();
                }
                // button handler
    ");
                WriteLiteral(@"            cancelBtn.click(function (e) {
                    e.preventDefault();
                    $('#userForm').validate().resetForm();
                    $('#name').val(name);
                    $('#nric').val(nric);
                    $('#phoneNumber').val(phoneNum);
                    $('#DOB').val(DOB);
                    console.log(""default gender=>"" + defaultGender);
                    $('#genderSelect').val(defaultGender);
                });

                closeBtn.click(function (e) {
                    e.preventDefault();
                    $('#app-text').remove();
                    alertMessage.addClass('hide-btn').removeClass('alert-success');
                });


                $('#genderSelect').change(function (e) {
                    e.preventDefault();
                    console.log(""gender selected"");
                    $(this).valid();
                    if ($(this).val() != """") {
                        $('#genderErr').text("""");
            ");
                WriteLiteral(@"        }
                })

                if (userId != null) {
                    console.log(""id is:"" + userId);
                    var path = ""User/getMedHistory"";
                    var emptyLabel = ""No Medical History Found."";
                    var count;
                    var column = [
                        { ""data"": ""id"" },
                        { ""data"": ""date"" },
                        { ""data"": ""slot"" },
                        { ""data"": ""service"" },
                        { ""data"": ""result"" },
                        {
                            ""data"": null,
                            render: function (obj) {
                                var formatted = obj.formattedMedicalPrescription;
                                if (formatted != null) {
                                    return formatted.replace(""/n"", ""\n"");
                                }
                                else {
                                    return null;
                 ");
                WriteLiteral(@"               }
                                //var newformatted = formatted.replace(""/n"", ""\n"");
                            }

                        },
                        { ""data"": ""doctorName"" },


                    ]
                    table = $('#medHistory-list-table').DataTable({
                        ""lengthChange"": false,
                        ""processing"": true, // for show progress bar
                        ""serverSide"": true, // for process server side
                        ""filter"": true, // this is for disable filter (search box)
                        ""ordering"": false,
                        ""scrollX"": true,
                        ""pageLength"": 5,

                        ""ajax"": {
                            ""url"": ""/User/getMedHistory"",
                            ""data"": { ""Id"": userId },
                            ""type"": ""POST"",
                            ""datatype"": ""json"",
                        },
                        ""drawCallback""");
                WriteLiteral(@": function (setting) {
                            // Here the response
                            var response = setting.json;
                            console.log(response);
                        },
                        columns: [
                            { ""data"": ""id"" },
                            { ""data"": ""date"" },
                            { ""data"": ""slot"" },
                            { ""data"": ""service"" },
                            { ""data"": ""result"" },
                            {
                                ""data"": null,
                                render: function (obj) {
                                    var formatted = obj.formattedMedicalPrescription;
                                    if (formatted != ""-"") {
                                        return formatted.replaceAll(""/n"", '<br>');
                                    }
                                    else {
                                        return ""No Medical Prescription Avail");
                WriteLiteral(@"able."";
                                    }
                                }

                            },
                            { ""data"": ""doctorName"" },


                        ],
                        ""language"": {
                            ""emptyTable"": emptyLabel,
                        }
                    });

                }

                //bring form back to ori data if save data is error
                var resetForm = () => {
                    $('#userForm').validate().resetForm();
                    $('#name').val(name);
                    $('#nric').val(nric);
                    $('#phoneNumber').val(phoneNum);
                    $('#DOB').val(DOB);
                }

                //form submission
                $('#userForm').submit(function (e) {
                    e.preventDefault();
                    if ($('#userForm').valid()) {
                        if (isAble == true) {
                            isAble = false;
      ");
                WriteLiteral(@"                      btnVisibility();
                            disableAll();
                        }
                        var path = ""/User/UpdateDetail"";
                        data = {
                            Id: $('#id').val(),
                            NRIC: $('#nric').val(),
                            FullName: $('#name').val(),
                            PhoneNumber: $('#phoneNumber').val(),
                            DOB: $('#DOB').val(),
                            Gender: $('#genderSelect option:selected').val()
                        };
                        defaultGender = $('#genderSelect option:selected').val();
                        ajaxPOST(data, path, '', '', res);
                        isAble = false;
                        btnVisibility();
                        console.log(`response returned=> ${res.response}`);
                        if (res.response = true) {
                            defaultGender = $('#genderSelect option:selected').val()");
                WriteLiteral(@";
                            $('#app-text').remove();
                            alertMessage.append(`<span id=""app-text"">Patient details updated successfully.</span>`);
                            alertMessage.addClass(""alert-success"").removeClass(""hide-btn"");
                        }
                        else {
                            $('#app-text').remove();
                            alertMessage.append(`<span id=""app-text"">Error while updating patient details.</span>`);
                            alertMessage.addClass(""alert-danger"").removeClass(""hide-btn"");
                            resetForm;
                        }
                    }
                });

                function AddMedHistory() {
                    var medHistory = $('#InputMedHistory').val();
                    var userId = $('#med-history-userId').val();
                    console.log(""description =>"" + medHistory + "" "" + ""user id =>"" + userId);
                    $.ajax({
                  ");
                WriteLiteral(@"      type: ""POST"",
                        url: ""/User/AddMedHistory"",
                        data: { ""Description"": medHistory, ""userId"": userId },
                        success: function (response) {
                            var res = response[""res""];
                            var message = response[""msg""];
                            table.ajax.reload();
                            $('.med-history-modal').modal(""hide"");
                            alertMessageFunc(res, message, medHisAlertMsg);

                        }
                    });
                }
                $('#add-MedHistory-btn').click(function (e) {
                    e.preventDefault();
                    AddMedHistory();
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<UserViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
