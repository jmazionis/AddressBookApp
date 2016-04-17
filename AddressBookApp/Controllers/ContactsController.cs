using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using AddressBookApp.Data.Context;
using AddressBookApp.ViewModels;
using AddressBookApp.Data.Interfaces;
using DataTables.Mvc;
using AddressBookApp.Helpers;
using AddressBookApp.Common.Utils.Models;
using AddressBookApp.Data.Infrastructure;
using System;

namespace AddressBookApp.Controllers
{
    public class ContactsController : Controller
    {
        private readonly IAddressBookUnitOfWork _unitOfWork;

        public ContactsController(IAddressBookUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public JsonResult GetFilteredContacts([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest dataTablesRequestModel)
        {
            int filteredCount;
            int totalCount;            

            var sortedColumns = dataTablesRequestModel.Columns.GetSortedColumns();
            var sortingConfig = DataTablesHelper.GetSortingConfig(sortedColumns);
            var searchConfig = DataTablesHelper.GetSearchingConfig(dataTablesRequestModel.Search.Value, dataTablesRequestModel.Columns);
            var filterConfig = new DataTablesFilteringModel
            {
                SearchConfig = searchConfig,
                SortingConfig = sortingConfig,
                StartIndex = dataTablesRequestModel.Start,
                ItemAmount = dataTablesRequestModel.Length,
            };          

            var filteredResults = _unitOfWork.PersonRepository.GetFilteredList(filterConfig, out filteredCount, out totalCount);

            var data = filteredResults.Select(contact => new ContactListItemViewModel
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

            return Json(new DataTablesResponse
            (
                dataTablesRequestModel.Draw,
                data,
                filteredCount,
                totalCount
            ), JsonRequestBehavior.AllowGet);
        }

        // GET: People
        public ActionResult Index()
        {           
            var contactListModels = _unitOfWork.PersonRepository.GetAll();
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
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Surname")] ContactFormViewModel createPersonViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _unitOfWork.PersonRepository.CreateNewPerson(new Person
                    {
                        Name = createPersonViewModel.Name,
                        Surname = createPersonViewModel.Surname
                    });
                    TempData["SuccessMessage"] = "Contact has been successfully created";
                    _unitOfWork.CommitChanges();
                }
                return View(createPersonViewModel);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An error occured while creating new contact";
                return View(createPersonViewModel);
            }                                                    
        }

        // GET: People/Edit/5
        public ActionResult Edit([Bind(Include = "Id")] int id)
        {
            var contactModel = _unitOfWork.PersonRepository.GetById(id);
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
                                   }).ToList(),
                Addresses = contactModel.Addresses
                                   .Select(address => new AddressViewModel
                                   {
                                       Id = address.Id,
                                       Name = address.Name,
                                       PersonId = address.PersonId
                                   }).ToList()                              
            };
            return View("Create", contactViewModel);
        }
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Surname,Emails,Addresses")] ContactFormViewModel editPersonViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _unitOfWork.PersonRepository.UpdatePerson(new Person
                    {
                        Id = editPersonViewModel.Id,
                        Name = editPersonViewModel.Name,
                        Surname = editPersonViewModel.Surname
                    });
                    _unitOfWork.CommitChanges();

                    TempData["SuccessMessage"] = "Contact has been successfully updated";                    
                }
                return View("Create", editPersonViewModel);
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "An error occured while editing contact";
                return View("Create", editPersonViewModel);
            }                                              
        }

        // GET: People/Delete/5
        public ActionResult Delete(int id)
        {
            var personModel = _unitOfWork.PersonRepository.GetById(id);
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
            try
            {
                _unitOfWork.PersonRepository.DeletePerson(id);
                _unitOfWork.CommitChanges();
                TempData["SuccessMessage"] = "Contact has been successfully deleted!";
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "An error occured while deleting contact";
                return RedirectToAction("Index");
            }           
        }      
    }
}
