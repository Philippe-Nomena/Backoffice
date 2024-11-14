(function ($) {
    "use strict";

    // Spinner
    var spinner = function () {
        setTimeout(function () {
            if ($('#spinner').length > 0) {
                $('#spinner').removeClass('show');
            }
        }, 1);
    };
    spinner();
    
    
    // Back to top button
    $(window).scroll(function () {
        if ($(this).scrollTop() > 300) {
            $('.back-to-top').fadeIn('slow');
        } else {
            $('.back-to-top').fadeOut('slow');
        }
    });
    $('.back-to-top').click(function () {
        $('html, body').animate({scrollTop: 0}, 1500, 'easeInOutExpo');
        return false;
    });


    // Sidebar Toggler
    $('.sidebar-toggler').click(function () {
        $('.sidebar, .content').toggleClass("open");
        return false;
    });


    // Progress Bar
    $('.pg-bar').waypoint(function () {
        $('.progress .progress-bar').each(function () {
            $(this).css("width", $(this).attr("aria-valuenow") + '%');
        });
    }, {offset: '80%'});


    // Calender
    $('#calender').datetimepicker({
        inline: true,
        format: 'L'
    });


    // Testimonials carousel
    $(".testimonial-carousel").owlCarousel({
        autoplay: true,
        smartSpeed: 1000,
        items: 1,
        dots: true,
        loop: true,
        nav : false
    });


    // Chart Global Color
    Chart.defaults.color = "#6C7293";
    Chart.defaults.borderColor = "#000000";

    async function renderPratiquantsChart() {
        const response = await fetch('/Pratiquants/GetPratiquantsChartData');
        const data = await response.json();
        console.log(data);

        // Create chart for Activité, Categorie, and Compagnie
        const chartConfig = [
            {
                data: data.ActiviteData,
                label: "Activité",
                elementId: 'activiteChart'
            },
            {
                data: data.CategorieData,
                label: "Catégorie",
                elementId: 'categorieChart'
            },
            {
                data: data.CompagnyData,
                label: "Compagnie",
                elementId: 'compagnyChart'
            }
        ];

        chartConfig.forEach(config => {
            const ctx = document.getElementById(config.elementId).getContext('2d');
            new Chart(ctx, {
                type: 'bar',
                data: {
                    labels: config.data.map(item => item[`${config.label}Id`]),
                    datasets: [{
                        label: config.label,
                        data: config.data.map(item => item.Count),
                        backgroundColor: 'rgba(0, 123, 255, 0.5)', 
                        borderColor: 'rgba(0, 123, 255, 1)',
                        borderWidth: 1
                    }]
                },
                options: {
                    responsive: true,
                    scales: {
                        y: {
                            beginAtZero: true
                        }
                    }
                }
            });
        });
    }



})(jQuery);

