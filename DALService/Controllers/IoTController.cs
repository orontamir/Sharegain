using Base.Models;
using Base.Services;
using DALService.DAL.Sql;
using DALService.Interfaces;
using DALService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace DALService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize]
    public class IoTController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ISignalService _signalService;
        public IoTController(IConfiguration configuration, ISignalService signalService) 
        {
            _configuration = configuration;
            _signalService = signalService;
        }


        
        [HttpPost("Create")]       
        public async Task<IActionResult> Create([FromBody] JObject collection)
        {
            try
            {
                double amplitude = 32.0;
                double max_range = 4095.0;
                double min_range = 256.0;
                Sin _sin = new Sin();
                SignalDbModel signalDbModel = new SignalDbModel
                {
                    Type = "Sin",
                    TimeStemp = (DateTime)collection["Time"],
                    Time = ((DateTime)(collection["Time"])).TimeOfDay.ToString(),
                    Date = ((DateTime)(collection["Time"])).Date.ToString(),
                    Value = (double)collection["Sin"],
                    Error = _sin.Validation((double)collection["Sin"], 0.0 , amplitude)
                };
                _signalService.AddSignal(signalDbModel);
                State _state = new State();
                 signalDbModel = new SignalDbModel
                {
                    Type = "State",
                    TimeStemp = (DateTime)collection["Time"],
                    Time = ((DateTime)(collection["Time"])).TimeOfDay.ToString(),
                    Date = ((DateTime)(collection["Time"])).Date.ToString(),
                    Value = (double)collection["State"],
                    Error = _state.Validation((double)collection["State"], 0.0, amplitude)
                };
                _signalService.AddSignal(signalDbModel);
                return Ok(signalDbModel);
            }
            catch(Exception ex) 
            {
                LogService.LogError($"Exception when insert signal into data base , error message: {ex.Message}");
                return BadRequest(ex);
            }
        }
        [HttpGet("GetSinList")]
        public async Task<string> GetSinList()
        {
            JArray jArray = new JArray();
            var sinList = await _signalService.GetSinList(500);
            foreach (var sin in sinList) 
            {
                JObject sinObj = new JObject(
                    new JProperty("c", new JArray(
                        new JObject(
                            new JProperty("v", sin.Time),
                            new JProperty("f", null)
                            ),
                        new JObject(
                            new JProperty("v", sin.Value),
                            new JProperty("f", null)
                            )
                        )
                    )
                    );
                jArray.Add( sinObj );
            }
            JArray colArray = new JArray();

            JObject jObject1 = new JObject(new JProperty("id", ""), new JProperty("label", "time"), new JProperty("pattern", ""), new JProperty("type", "string"));
            JObject jObject2 = new JObject(new JProperty("id", ""), new JProperty("label", "Value"), new JProperty("pattern", ""), new JProperty("type", "number"));
            colArray.Add(jObject1);
            colArray.Add(jObject2);
            JObject json = new JObject(new JProperty("cols", colArray), new JProperty("rows", jArray));


            return JsonConvert.SerializeObject(json);

        }

        [HttpGet("GetStateList")]
        public async Task<string> GetStaetList()
        {
            JObject jArray = new JObject();
            var stateList = await _signalService.GetStateList(500);

            foreach (var sine in stateList)
            {
                // dataQuery.Rows[i][0], dataQuery.Rows[i][1]
                JObject row = new JObject(
                    new JProperty("c", new JArray(
                            new JObject(
                                new JProperty("v", sine.Time),
                                new JProperty("f", null)),
                            new JObject(
                                new JProperty("v", sine.Value),
                                new JProperty("f", null)
                                )
                            )
                    )
                );
                jArray.Add(row);
            }
            JArray colArray = new JArray();

            JObject jObject1 = new JObject(new JProperty("id", ""), new JProperty("label", "time"), new JProperty("pattern", ""), new JProperty("type", "string"));
            JObject jObject2 = new JObject(new JProperty("id", ""), new JProperty("label", "Value"), new JProperty("pattern", ""), new JProperty("type", "number"));
            colArray.Add(jObject1);
            colArray.Add(jObject2);
            JObject json = new JObject(new JProperty("cols", colArray), new JProperty("rows", jArray));


            return JsonConvert.SerializeObject(json);
        }

        [HttpGet("GetErrorList")]
        public async Task<string> GetErrorList()
        {
            JArray jArray = new JArray();
            var ErrorList = await _signalService.GetErrorList(500);
            foreach (var error in ErrorList)
            {
                JObject row = new JObject(
                                    new JProperty("c", new JArray(
                new JObject(
                                                new JProperty("v", error.Date),
                                                new JProperty("f", null)),
                                            new JObject(
                new JProperty("v", error.Type),
                                                new JProperty("f", null)),
                                            new JObject(
                new JProperty("v", error.Value),
                                                new JProperty("f", null))
                                            )
                                    )
                                );
                jArray.Add(row);
            }

            JArray colArray = new JArray();
            JObject jObject1 = new JObject(new JProperty("id", ""), new JProperty("label", "Date"), new JProperty("pattern", ""), new JProperty("type", "string"));
            JObject jObject2 = new JObject(new JProperty("id", ""), new JProperty("label", "Type"), new JProperty("pattern", ""), new JProperty("type", "string"));
            JObject jObject3 = new JObject(new JProperty("id", ""), new JProperty("label", "Value"), new JProperty("pattern", ""), new JProperty("type", "string"));

            colArray.Add(jObject1);
            colArray.Add(jObject2);
            colArray.Add(jObject3);
            JObject json = new JObject(new JProperty("cols", colArray), new JProperty("rows", jArray));


            return JsonConvert.SerializeObject(json);
        }


    }

    
}
