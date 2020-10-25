using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using masterDatabase;
using System.Web.Http.Cors;

namespace page2APIdemo.Controllers
{

[EnableCors(origins: "http://localhost:54720", headers:"*",methods:"*")]

public class GetMethodController : ApiController
{
    masterEntities1 spApiBranch = new masterEntities1();


    [HttpPost]
    public List<spApiBranch_Result> GetBranch([FromBody] spApiBranch_Result [] sysParam ) { //NORMAL WAY
        List<spApiBranch_Result> sysData = spApiBranch.spApiBranch(sysParam[0].function, sysParam[0].branchCode).ToList();
        if (sysData.Count == 0)
        {
            sysData.Add(new spApiBranch_Result
            {
                retValue = false,
                retStatus = "nothing",
                retMessage = "Data not found."
            });
        }
        return sysData;
    }

    [HttpPost]
    public HttpResponseMessage GetBranch1([FromBody] spApiBranch_Result[] sysParam) // STANDARD WAY
    {
        List<spApiBranch_Result> sysData = spApiBranch.spApiBranch(sysParam[0].function,sysParam[0].branchCode).ToList();
        if (sysData.Count > 0)
        {
            return Request.CreateResponse(HttpStatusCode.OK, sysData);
        }
        else {
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Data not found.");
        }
    }

    public HttpResponseMessage GetBranchByName([FromBody] spApiBranch_Result[] sysParam) // STANDARD WAY
    {
        List<spApiBranch_Result> sysData = spApiBranch.spApiBranch(sysParam[0].function, sysParam[0].bran).ToList();
        if (sysData.Count > 0)
        {
            return Request.CreateResponse(HttpStatusCode.OK, sysData);
        }
        else
        {
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Data not found.");
        }
    }

}
}
