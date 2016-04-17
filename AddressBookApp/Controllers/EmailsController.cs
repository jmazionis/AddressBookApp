using System.Web.Mvc;
using AddressBookApp.Data.Context;
using AddressBookApp.ViewModels;
using AddressBookApp.Data.Interfaces;
using AddressBookApp.Data.Infrastructure;
using System;

namespace AddressBookApp.Controllers
{
    public class EmailsController : Controller
    {
        private readonly IAddressBookUnitOfWork _unitOfWork;

        public EmailsController(IAddressBookUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        public ActionResult Create(int id)
        {
            return View(new EmailViewModel
            {
                PersonId = id
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name,PersonId")] EmailViewModel emailViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _unitOfWork.EmailRepository.AddNewEmailToContact(new Email
                    {
                        PersonId = emailViewModel.PersonId,
                        Name = emailViewModel.Name
                    });
                    _unitOfWork.CommitChanges();
                    TempData["SuccessMessage"] = "New email has been successfully added!";
                    return RedirectToAction("Edit", "Contacts", new { id = emailViewModel.PersonId });
                }
                return View(emailViewModel);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An error occured while creating new email";
                return View(emailViewModel);
            }
          
        }
        
        public ActionResult Edit([Bind(Include = "emailId")] int emailId)
        {
            var emailModel = _unitOfWork.EmailRepository.GetById(emailId);            
            return View(new EmailViewModel
            {
                Id = emailModel.Id,
                Name = emailModel.Name,
                PersonId = emailModel.PersonId
            });
        }
      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,PersonId")] EmailViewModel emailViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _unitOfWork.EmailRepository.UpdateEmail(
                    new Email
                    {
                        Id = emailViewModel.Id,
                        Name = emailViewModel.Name,
                        PersonId = emailViewModel.PersonId
                    });
                    _unitOfWork.CommitChanges();
                    TempData["SuccessMessage"] = "Email has been successfully updated!";
                    return RedirectToAction("Edit", "Contacts", new { id = emailViewModel.PersonId });
                }
                return View(emailViewModel);
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "An error occured while updating email";
                return View(emailViewModel);
            }           
        }

        // GET: Emails/Delete/5
        public ActionResult Delete(int emailId)
        {
            var targetEmail = _unitOfWork.EmailRepository.GetById(emailId);
            return PartialView("Partials/Emails/_DeleteEmailModal", new EmailViewModel
            {
                Id = targetEmail.Id,
                Name = targetEmail.Name,
                PersonId = targetEmail.PersonId
            });
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete([Bind(Include = "Id,PersonId,Name")] EmailViewModel emailViewModel)
        {
            try
            {
                _unitOfWork.EmailRepository.DeleteEmail(emailViewModel.Id);
                _unitOfWork.CommitChanges();
                TempData["SuccessMessage"] = "Email has been successfully deleted";
                return RedirectToAction("Edit", "Contacts", new { id = emailViewModel.PersonId });
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "An error occured while deleting email";
                return RedirectToAction("Edit", "Contacts", new { id = emailViewModel.PersonId });
            }
           
        }
    }
}
