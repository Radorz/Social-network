// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


$('#exampleModal').on('show.bs.modal', function (event) {
	var button = $(event.relatedTarget) 
	var recipient = button.data('whatever')
	var id = button.data('id')
	var modal = $(this)
	modal.find('.modal-body textarea').val(recipient)
	modal.find('.modal-body .hola').val(id)

})

$('#exampleModal2').on('show.bs.modal', function (event) {
	var button = $(event.relatedTarget) 
	var id = button.data('id')
	var modal = $(this)
	modal.find('.modal-body .hola').val(id)

})

function comprobar() {
	if (document.getElementById("textsend").value.trim() == "") {
		document.getElementById('button').disabled = true;
	} else {
		document.getElementById('button').disabled = false;
	}
}

function validar() {

	var val = document.querySelectorAll('textarea').value;

	if (val.trim() == "") {
		return false;
	}

	function limpiarFormulario() {
		document.getElementById("form").reset();
		return true;

	}
}