using System.Web.Mvc;
using AddressBookApp.Data.Context;
using AddressBookApp.ViewModels;
using AddressBookApp.Data.Interfaces;

namespace AddressBookApp.Controllers
{
    public class AddressesController : Controller
    {
        private readonly IAddressRepository _addressRepository;

        public AddressesController(IAddressRepository addressRepository)
        {
            _addressRepository = addressRepository;
        }                       

        // GET: Addresses/Create
        public ActionResult Create(int id)
        {
            return View(new AddressViewModel
            {
                PersonId = id
            });
        }
      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name,PersonId")] AddressViewModel addressViewModel)
        {
            if (ModelState.IsValid)
            {
                _addressRepository.AddNewAddressToContact(new Address
                {
                    PersonId = addressViewModel.PersonId,
                    Name = addressViewModel.Name
                });
                TempData["SuccessMessage"] = "Address has been successfully added!";
                return RedirectToAction("Edit", "Contacts", new { id = addressViewModel.PersonId });
            }            

            return View(addressViewModel);
        }

        // GET: Addresses/Edit/5
        public ActionResult Edit([Bind(Include = "addressId")] int addressId)
        {
            var addressModel = _addressRepository.GetById(addressId);           
            return View(new AddressViewModel
            {
                Id = addressModel.Id,
                Name = addressModel.Name,
                PersonId = addressModel.PersonId
            });
        }
      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,PersonId")] AddressViewModel addressViewModel)
        {
            if (ModelState.IsValid)
            {
                _addressRepository.UpdateAddress(
                new Address
                {
                    Id = addressViewModel.Id,
                    Name = addressViewModel.Name,
                    PersonId = addressViewModel.PersonId
                });
                TempData["SuccessMessage"] = "Address has been successfully updated!";
                return RedirectToAction("Edit", "Contacts", new { id = addressViewModel.PersonId });
            }
            return View(addressViewModel);
        }
     
        public ActionResult Delete(int addressId)
        {
            var targetAddress = _addressRepository.GetById(addressId);
            return PartialView("Partials/Addresses/_DeleteAddressModal", new AddressViewModel
            {
                Id = targetAddress.Id,
                Name = targetAddress.Name,
                PersonId = targetAddress.PersonId
            });
        }
        
        [HttpPost]        
        [ValidateAntiForgeryToken]
        public ActionResult Delete([Bind(Include = "Id,PersonId,Name")] AddressViewModel addressViewModel)
        {
            _addressRepository.DeleteAddress(addressViewModel.Id);
            TempData["SuccessMessage"] = "Address has been successfully deleted";
            return RedirectToAction("Edit", "Contacts", new { id = addressViewModel.PersonId });
        }
    }
}
