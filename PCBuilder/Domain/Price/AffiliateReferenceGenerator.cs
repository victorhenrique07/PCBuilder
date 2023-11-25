using PCBuilder.Domain.PostSchedule;
using System;
using System.Net;

namespace PCBuilder.Domain.Price
{
    public class AffiliateReferenceGenerator
    {
        protected AffiliateReferenceGenerator() { }

        public virtual string Replaceables { get; protected set; }

        public virtual string Replacements { get; protected set; }

        public virtual string Preffix { get; protected set; }

        public virtual string Suffix { get; protected set; }

        public virtual string CutAfter { get; protected set; }

        public virtual bool EncodeReference { get; protected set; }

        public string GenerateAffiliateReference(string reference, SocialMedia targetMedia)
        {
            if (string.IsNullOrWhiteSpace(reference))
                throw new ArgumentNullException(nameof(reference));

            string affiliateReference = reference;

            if (this.Replaceables != null)
            {
                var replaceables = this.Replaceables.Split(';');
                var replacements = this.Replacements.Split(';');

                for (int i = 0; i < replacements.Length; i++)
                {
                    affiliateReference = affiliateReference.Replace(replaceables[i], replacements[i]);
                }
            }

            if (!string.IsNullOrEmpty(this.Preffix))
            {
                if (this.EncodeReference)
                    affiliateReference = this.Preffix + WebUtility.UrlEncode(affiliateReference);
                else
                    affiliateReference = this.Preffix + affiliateReference;
            }

            if (!string.IsNullOrEmpty(this.CutAfter) && affiliateReference.Contains(this.CutAfter))
                affiliateReference = affiliateReference.Substring(0, affiliateReference.IndexOf(this.CutAfter));

            if (!string.IsNullOrEmpty(this.Suffix))
            {
                affiliateReference += this.Suffix;
            }

            /* TODO: refactor */
            if (targetMedia != null)
            {
                string amazonBaseTrackingId = "pcbuildwizard-20";

                if (affiliateReference.Contains(amazonBaseTrackingId))
                {
                    affiliateReference = affiliateReference.Insert(affiliateReference.IndexOf("-20"),
                        $"-{targetMedia.Code}");
                }
                else
                {
                    affiliateReference += targetMedia.ShortName;
                }
            }

            return affiliateReference;
        }
    }
}
