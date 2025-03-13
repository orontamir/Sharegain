using LogoManager.Extensions;
using LogoManager.Interface;
using LogoManager.Interfaces;
using LogoManager.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LogoManager.Controllers
{
    public class LogoMessageController : Controller
    {
        private readonly ITCPService _Service;
        private readonly ILogMessageService _LogMessageService;
        public LogoMessageController(ITCPService service, ILogMessageService logMessageService) { 
            _Service = service;
            _LogMessageService = logMessageService;
        }
        

        // GET: LogoMessageController/Start
        public ActionResult Start()
        {
            _Service.Start(1313, "0.0.0.0");
            return View();
        }

        // GET: LogoMessageController/Stop
        public ActionResult Stop()
        {
            _Service.Stop();
            return View();
        }

        // POST: LogoMessageController/Insert
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Insert(LogoMessageModel model)
        {
            try
            {
                var newEntity = await _LogMessageService.InsertLogMessage(model.ToEntity());
                return RedirectToAction(nameof(newEntity));
            }
            catch
            {
                return View();
            }
        }
    }
}
