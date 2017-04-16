using SkinnedWebChat.Bot.Areas.Bot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SkinnedWebChat.Bot.Areas.Bot.Controllers
{
    public class WebChatController : Controller
    {
        // GET: Bot/WebChat
        public async Task<ActionResult> Index()
        {
            var vm = new WebChatModel();
            await vm.SetToken();
            return View(vm);
        }
        public async Task<ActionResult> iFramed()
        {
            var vm = new WebChatModel();
            await vm.SetToken();
            return View(vm);
        }
    }
}