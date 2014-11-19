﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ToDoWebApp.Models;

namespace ToDoWebApp.Controllers
{
    public class ItemController : Controller
    {
        public ActionResult Index()
        {
            var items = DocumentDBRepository.GetIncompleteItems();
            return View(items);
        }
        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        // To protect against Cross-Site Request Forgery, validate that the anti-forgery token was received and is valid
        // for more details on preventing see http://go.microsoft.com/fwlink/?LinkID=517254
        [ValidateAntiForgeryToken]

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        public async Task<ActionResult> Create([Bind(Include = "Id,Name,Description,Completed")] Item item)
        {
            if (ModelState.IsValid)
            {
                await DocumentDBRepository.CreateItemAsync(item);
                return RedirectToAction("Index");
            }

            return View(item);
        }


        [HttpPost]

        // To protect against Cross-Site Request Forgery, validate that the anti-forgery token was received and is valid
        // for more details on preventing see http://go.microsoft.com/fwlink/?LinkID=517254
        [ValidateAntiForgeryToken]

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.        
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,Description,Completed")] Item item)
        {
            if (ModelState.IsValid)
            {
                await DocumentDBRepository.UpdateItemAsync(item);
                return RedirectToAction("Index");
            }

            return View(item);
        }

        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Item item = (Item)DocumentDBRepository.GetItem(id);
            if (item == null)
            {
                return HttpNotFound();
            }

            return View(item);
        }

        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Item item = (Item)DocumentDBRepository.GetItem(id);
            if (item == null)
            {
                return HttpNotFound();
            }

            return View(item);
        }

        [HttpPost, ActionName("Delete")]
        // To protect against Cross-Site Request Forgery, validate that the anti-forgery token was received and is valid
        // for more details on preventing see http://go.microsoft.com/fwlink/?LinkID=517254
        [ValidateAntiForgeryToken]

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        public async Task<ActionResult> DeleteConfirmed([Bind(Include = "Id")] string id)
        {
            await DocumentDBRepository.DeleteItemAsync(id);
            return RedirectToAction("Index");
        }

        public ActionResult Details(string id)
        {
            Item item = DocumentDBRepository.GetItem(id);
            return View(item);
        }
    }
}