function SetModal() {

    $(document).ready(function () {
        $(function () {
            $.ajaxSetup({ cache: false });

            $("a[data-modal]").on("click",
                function (e) {
                    $('#myModalContent').load(this.href,
                        function () {
                            $('#myModal').modal({
                                keyboard: true
                            },
                                'show');
                            bindForm(this);
                        });
                    return false;
                });
        });
    });
}

function bindForm(dialog) {
    $('form', dialog).submit(function () {
        $.ajax({
            url: this.action,
            type: this.method,
            data: $(this).serialize(),
            success: function (result) {
                if (result.success) {
                    $('#myModal').modal('hide');
                    $('#AddressTarget').load(result.url); // Carrega o resultado HTML para a div demarcada
                } else {
                    $('#myModalContent').html(result);
                    bindForm(dialog);
                }
            }
        });

        SetModal();
        return false;
    });
}

/* this function would find a address by postal code, so it depends on the country
function searchPostalCode() {
    $(document).ready(function () {

        function clear_form_postalCode() {
            // Limpa valores do formulário de cep.
            $("#Address_PublicPlace").val("");
            $("#Address_District").val("");
            $("#Address_City").val("");
            $("#Address_State").val("");
        }

        //When the fild postal code loses focus.
        $("#Address_PostalCode").blur(function () {

            //New variable "postaColde" only digits.
            var postalCode = $(this).val().replace(/\D/g, '');

            //Verifies if postalCode has the valor informed.
            if (postalCode != "") {

                //Regular Expression to validate PostalCode.
                var validPostalCode = /^[0-9]{8}$/;

                //Validates the format of postalCode.
                if (validPostalCode.test(postalCode)) {

                    //Fill in the fields with "..." while requests the webservice.
                    $("#Address_PublicPlace").val("...");
                    $("#Address_District").val("...");
                    $("#Address_City").val("...");
                    $("#Address_State").val("...");

                    //Replace to the o webservice you need
                    $.getJSON("https://viacep.com.br/ws/" + cep + "/json/?callback=?",
                        function (data) {

                            if (!("erro" in data)) {
                                //Update the fields with the data from the request.
                                $("#Address_PublicPlace").val(data.);
                                $("#Address_District").val(data.);
                                $("#Address_City").val(data.);
                                $("#Address_State").val(data.);
                            } //end if.
                            else {
                                //No results found.
                                limpa_formulário_cep();
                                alert("CEP não encontrado.");
                            }
                        });
                } //end if.
                else {
                    //cep é inválido.
                    clear_form_postalCode();
                    alert("Invalid Postal code format.");
                }
            } //end if.
            else {
                //Postal code whithout data, clears form.
                clear_form_postalCode();
            }
        });
    });
} */

$(document).ready(function () {
    $("#msg_box").fadeOut(2500);
});