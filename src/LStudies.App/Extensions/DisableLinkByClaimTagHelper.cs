﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;

namespace LStudies.App.Extensions
{
    [HtmlTargetElement("*", Attributes = "disable-by-claim-name")]
    [HtmlTargetElement("*", Attributes = "disable-by-claim-value")]
    public class DisableLinkByClaimTagHelper : TagHelper
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public DisableLinkByClaimTagHelper(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        [HtmlAttributeName("disable-by-claim-name")]
        public string IdentityClaimName { get; set; }

        [HtmlAttributeName("disable-by-claim-value")]
        public string IdentityClaimValue { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }
            if (output == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var hasAccess = CustomAuthorization.ValidateUserClaims(_contextAccessor.HttpContext, IdentityClaimName, IdentityClaimValue);

            if (hasAccess) return;

            output.Attributes.RemoveAll("href");
            output.Attributes.Add(new TagHelperAttribute("style", "cursor: not-allowed"));
            output.Attributes.Add(new TagHelperAttribute("title", "No permissions"));
        }
    }
}
