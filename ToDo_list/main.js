if (localStorage.getItem('mn') == null) {
    localStorage.setItem('mn', 1);
}

$(document).ready(function()
{
    
    var modal = $(".modal"); 
    var modal2 = $(".modal2");
    var hid = $(".hid"); 
    var i;
    var height = $('#hg').attr('data-attr');
    var b = $('body');
    if (localStorage.getItem('mn') == 2)
    {
        b.css({'background-color': '#7fb7ec'});
        $('#ppp').css({'color': 'black'});
        $('.pp').css({'color': 'black'});
        $('#theme1').css({'box-shadow': '0 0 40px 40px #040b70 inset, 0 0 0 0 #008000'});
    }
    else if (localStorage.getItem('mn') == 1)
    {
        b.css({'background-color': '#040b70'});
        $('#ppp').css({'color': 'white'});
        $('.pp').css({'color': 'white'});
        $('#theme1').css({'box-shadow': '0 0 40px 40px #7fb7ec inset, 0 0 0 0 #008000'});
    }

    $( window ).resize(function(){ 
	 if ($(window).width() <= 1250)
    {
        $('.list').css({'height':height * 120});
    }
    else{
        $('.list').css({'height':height * 75});
    }
	  });

    $('#Send').on('click', function(){   
        modal.fadeIn(200);      
    })

    $('.close').on('click', function(){  
        modal.fadeOut(200);    
        modal2.fadeOut(200);
    })

    $('.ph').on('click', function(){   
        modal2.fadeIn(200); 
        i = $(this).attr('data-attr');
        hid.attr('value', i);
    })


    $('.custom').change(function() {    
        if(this.checked){
            $(this).attr('data-attr', 1);
        }else{
            $(this).attr('data-attr', 0);
        }
    })

    $('.notupdated').on('click', function(){   
        $(this).attr("class", "updated");
        console.log("notupd");
    })

    $('.updated').on('click', function(){   
        $(this).attr("class", "notupdated");
        console.log("upd");
    })

    $('#theme1').on('click', function(){   
        if (localStorage.getItem('mn') == 1)
    {
        b.css({'background-color': '#7fb7ec'});
        $('#ppp').css({'color': 'black'});
        $('#theme1').css({'box-shadow': '0 0 40px 40px #040b70 inset, 0 0 0 0 #008000'});
        localStorage.setItem('mn', 2);
        console.log("b "+localStorage.getItem('mn'));
    }
    else if (localStorage.getItem('mn') == 2)
    {
        b.css({'background-color': '#040b70'});
        $('#ppp').css({'color': 'white'});
        $('#theme1').css({'box-shadow': '0 0 40px 40px #7fb7ec inset, 0 0 0 0 #008000'});
        localStorage.setItem('mn', 1);
        console.log("b "+localStorage.getItem('mn'));
    }
    })
});

var modal2 = document.querySelector(".modal2");

