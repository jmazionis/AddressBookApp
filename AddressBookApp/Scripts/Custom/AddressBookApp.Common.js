var AddressBookApp = (function () {

	var displayConfirmationMessage = function(message) {
		var $target = $('#confirmation-message .alert');
		$target.text(message);
		$target.fadeOut(5000, function() {
			$('#confirmation-message').remove();
		});
	};		
	return {
		displayConfirmationMessage: displayConfirmationMessage
	};

})() || {};