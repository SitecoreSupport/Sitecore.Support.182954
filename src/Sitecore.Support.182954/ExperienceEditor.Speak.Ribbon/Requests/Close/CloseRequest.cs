// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CloseRequest.cs" company="Sitecore A/S">
//   Copyright (C) 2014 by Sitecore A/S
// </copyright>
// <summary>
//   The close request.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Sitecore.Support.ExperienceEditor.Speak.Ribbon.Requests.Close
{
  using Sitecore.Diagnostics;
  using Sitecore.ExperienceEditor.Speak.Server.Contexts;
  using Sitecore.ExperienceEditor.Speak.Server.Requests;
  using Sitecore.ExperienceEditor.Speak.Server.Responses;
  using Sitecore.Text;
  using Sitecore.Web;

  /// <summary>
  /// The close request.
  /// </summary>
  public class CloseRequest : PipelineProcessorRequest<StringContext>
  {
    /// <summary>
    /// The _ mode query string.
    /// </summary>
    private const string ModeQueryString = "sc_mode";

    /// <summary>
    /// The _ normal mode.
    /// </summary>
    private const string NormalMode = "normal";

    /// <summary>
    /// The process request.
    /// </summary>
    /// <returns>
    /// The <see cref="PipelineProcessorResponseValue"/>.
    /// </returns>
    public override PipelineProcessorResponseValue ProcessRequest()
    {
      Assert.IsNotNullOrEmpty(this.RequestContext.Value, "Could not get string value for requestArgs:{0}", this.Args.Data);

      var url = new UrlString(this.RequestContext.Value);

      var sessionValueKey = url["sc_de"];
      if (!string.IsNullOrEmpty(sessionValueKey))
      {
        WebUtil.RemoveSessionValue(sessionValueKey);
        url.Remove("sc_de");
      }

      url[ModeQueryString] = NormalMode;

      return new PipelineProcessorResponseValue { Value = url.ToString() };
    }
  }
}