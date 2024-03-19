$(function () {
    $('form').submit( e => {
        e.preventDefault();
        const q = $('#search').val();
        $('tbody').load('/Ranks/Search2?query=' + q);
        //const response = await fetch('/Ranks/Search2?query=' + q);
        //const data = await response.json();
        //$('tbody').html('<tr><td>' + data[0].UserName + '</tr></td>');

        //fetch('/Ranks/Search3?query=' + q)
        //    .then((response) => response.json())
        //    .then(data => console.log(data));
    })
}
);