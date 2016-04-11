using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using AddressBookApp.Data.Context;
using AddressBookApp.ViewModels;
using AddressBookApp.Data.Interfaces;

namespace AddressBookApp.Controllers
{
    public class ContactsController : Controller
    {

        private readonly IPersonRepository _personRepository;

        public ContactsController(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        // GET: People
        public ActionResult Index()
        {           
            var contactListModels = _personRepository.GetAll();
            var contactListViewModels = contactListModels.Select(contact => new ContactListItemViewModel
            {
                Id = contact.Id,
                Name = contact.Name,
                Surname = contact.Surname,
                Emails = contact.Emails
                                    .Select(email => new EmailViewModel
                                    {
                                        Id = email.Id,
                                        Name = email.Name,
                                        PersonId = email.PersonId
                                    }),
                Addresses = contact.Addresses
                                    .Select(address => new AddressViewModel
                                    {
                                        Id = address.Id,
                                        Name = address.Name,
                                        PersonId = address.PersonId
                                    })
            });                                                                
            return View(contactListViewModels);
        }        
        
        public ActionResult Create()
        {
            var contactFormViewModel = new ContactFormViewModel
            {
                Emails = new List<EmailViewModel>(),
                Addresses = new List<AddressViewModel>()        
            };

            return View(contactFormViewModel);
            //return PartialView("Partials/Contacts/_ContactFormModal");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Surname")] ContactFormViewModel createPersonViewModel)
        {            
            if (ModelState.IsValid)
            {
                _personRepository.CreateNewPerson(new Person
                {
                    Name = createPersonViewModel.Name,
                    Surname = createPersonViewModel.Surname
                });
            }
            TempData["SuccessMessage"] = "Contact has been successfully created";
            return View(createPersonViewModel);
        }

        // GET: People/Edit/5
        public ActionResult Edit([Bind(Include = "Id")] int id)
        {
            var contactModel = _personRepository.GetById(id);
            var contactViewModel = new ContactFormViewModel
            {
                Id = contactModel.Id,
                Name = contactModel.Name,
                Surname = contactModel.Surname,
                Emails = contactModel.Emails
                                   .Select(email => new EmailViewModel
                                   {
                                       Id = email.Id,
                                       Name = email.Name,
                                       PersonId = email.PersonId
                                   }),
                Addresses = contactModel.Addresses
                                   .Select(address => new AddressViewModel
                                   {
                                       Id = address.Id,
                                       Name = address.Name,
                                       PersonId = address.PersonId
                                   })                               
            };
            return View("Create", contactViewModel);
        }
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Surname")] ContactFormViewModel editPersonViewModel)
        {
            if (ModelState.IsValid)
            {
                _personRepository.UpdatePerson(new Person
                {
                    Id = editPersonViewModel.Id,
                    Name = editPersonViewModel.Name,
                    Surname = editPersonViewModel.Surname
                });
                TempData["SuccessMessage"] = "Contact has been successfully updated";
            }
            
            return View("Create", editPersonViewModel);
        }

        // GET: People/Delete/5
        public ActionResult Delete(int id)
        {
            var personModel = _personRepository.GetById(id);
            return PartialView("Partials/Contacts/Modals/_DeleteContactModal", 
                new ContactListItemViewModel
                {
                    Id = personModel.Id,
                    Name = personModel.Name,
                    Surname = personModel.Surname
                });
        }

        // POST: People/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmation([Bind(Include = "Id")]int id)
        {
            _personRepository.DeletePerson(id);
            TempData["SuccessMessage"] = "Contact has been successfully deleted!";
            return RedirectToAction("Index");
        }      
    }
}
