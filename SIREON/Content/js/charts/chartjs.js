$(function () {
  'use strict';

  if ($("#chartjs-staked-area-chart").length) {
    var options = {
      type: 'line',
      data: {
        labels: ["Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre","Diciembre" ],
        datasets: [{
            label: '# of Votes',
            data: [12, 19, 3, 5, 2, 3, 12, 19, 3, 5, 2, 3],
            backgroundColor: chartColors[0],
            borderColor: chartColors[0],
            borderWidth: 1,
          },
          {
            label: '# of Points',
            data: [100, 200, 300, 400, 500],
            borderColor: chartColors[1],
            borderWidth: 1,
            backgroundColor: chartColors[1]
          }
        ]
      },
      options: {
        scales: {
          yAxes: [{
            ticks: {
              reverse: false
            }
          }]
        },
        legend: false
      }
    }

    var ctx = document.getElementById('chartjs-staked-area-chart').getContext('2d');
    new Chart(ctx, options);
  }

  if ($("#chartjs-staked-line-chart").length) {
    var options = {
      type: 'line',
      data: {
          labels: ["Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre"],
        datasets: [{
            label: '# Estudiantes atentidos',
            data: [48, 61, 35, 25, 30, 55, 61, 35, 25, 30, 20, 10],
            borderWidth: 5,
            fill: false,
            backgroundColor: chartColors[3],
            borderColor: chartColors[3],
            borderWidth: 0
          },
          {
            label: '# Estudiantes no atendidos',
            data: [55, 71, 5, 8, 10, 45, 75, 7, 10, 6, 2, 0],
            borderWidth: 5,
            fill: false,
            backgroundColor: chartColors[2],
            borderColor: chartColors[2],
            borderWidth: 0
          }
        ]
      },
      options: {
        scales: {
          yAxes: [{
            ticks: {
              reverse: false
            }
          }]
        },
        fill: false,
        legend: false
      }
    }

    var ctx = document.getElementById('chartjs-staked-line-chart').getContext('2d');
    new Chart(ctx, options);
  }

  if ($("#chartjs-staked-line-chart2").length) {
        var options = {
            type: 'line',
            data: {
                labels: ["Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre"],
                datasets: [{
                    label: '# Estudiantes atentidos',
                    data: [48, 61, 35, 25, 30, 55, 61, 35, 25, 30, 20, 10],
                    borderWidth: 5,
                    fill: false,
                    backgroundColor: chartColors[3],
                    borderColor: chartColors[3],
                    borderWidth: 0
                },
                {
                    label: '# Estudiantes no atendidos',
                    data: [55, 71, 5, 8, 10, 45, 75, 7, 10, 6, 2, 0],
                    borderWidth: 5,
                    fill: false,
                    backgroundColor: chartColors[2],
                    borderColor: chartColors[2],
                    borderWidth: 0
                }
                ]
            },
            options: {
                scales: {
                    yAxes: [{
                        ticks: {
                            reverse: false
                        }
                    }]
                },
                fill: false,
                legend: false
            }
        }

        var ctx = document.getElementById('chartjs-staked-line-chart2').getContext('2d');
        new Chart(ctx, options);
    }



  if ($("#chartjs-bar-chart").length) {
    var BarData = {
      labels: ["2013", "2014", "2014", "2015", "2016", "2017"],
      datasets: [{
        label: '# of Votes',
        data: [10, 19, 3, 5, 12, 3],
        backgroundColor: chartColors,
        borderColor: chartColors,
        borderWidth: 0
      }]
    };
    var barChartCanvas = $("#chartjs-bar-chart").get(0).getContext("2d");
    var barChart = new Chart(barChartCanvas, {
      type: 'bar',
      data: BarData,
      options: {
        legend: false
      }
    });
  }

  if ($("#chartjs-staked-bar-chart").length) {
    var BarData = {
      labels: ["2013", "2014", "2014", "2015", "2016", "2017"],
      datasets: [{
          label: 'Profit',
          data: [10, 19, 3, 5, 12, 3],
          backgroundColor: chartColors[0],
          borderColor: chartColors[0],
          borderWidth: 0
        },
        {
          label: 'Sales',
          data: [23, 12, 8, 13, 9, 17],
          backgroundColor: chartColors[1],
          borderColor: chartColors[1],
          borderWidth: 0
        }
      ]
    };
    var barChartCanvas = $("#chartjs-staked-bar-chart").get(0).getContext("2d");
    var barChart = new Chart(barChartCanvas, {
      type: 'bar',
      data: BarData,
      options: {
        tooltips: {
          mode: 'index',
          intersect: false
        },
        responsive: true,
        scales: {
          xAxes: [{
            stacked: true,
          }],
          yAxes: [{
            stacked: true
          }]
        },
        legend: false
      }
    });
  }

  if ($("#chartjs-radar-chart").length) {
    var marksCanvas = document.getElementById("chartjs-radar-chart");

    var marksData = {
      labels: ["English", "Maths", "Physics", "Chemistry", "Biology", "History"],
      datasets: [{
          label: "Student A",
          data: [24, 55, 30, 56, 60, 68],
          backgroundColor: chartColors[0],
          borderColor: chartColors[0],
          borderWidth: 0
        }, {
          label: "Student B",
          data: [54, 65, 60, 70, 70, 75],
          backgroundColor: chartColors[1],
          borderColor: chartColors[1],
          borderWidth: 0
        },
        {
          label: "Student c",
          data: [43, 13, 33, 57, 50, 75],
          backgroundColor: chartColors[2],
          borderColor: chartColors[2],
          borderWidth: 0
        }
      ]
    };

    var radarChart = new Chart(marksCanvas, {
      type: 'radar',
      data: marksData
    });
  }

  if ($("#chartjs-doughnut-chart").length) {
    var DoughnutData = {
      datasets: [{
        data: [30, 40, 30],
        backgroundColor: chartColors,
        borderColor: chartColors,
        borderWidth: chartColors
      }],
      labels: [
        'Data 1',
        'Data 2',
        'Data 3',
      ]
    };
    var DoughnutOptions = {
      responsive: true,
      animation: {
        animateScale: true,
        animateRotate: true
      }
    };
    var doughnutChartCanvas = $("#chartjs-doughnut-chart").get(0).getContext("2d");
    var doughnutChart = new Chart(doughnutChartCanvas, {
      type: 'doughnut',
      data: DoughnutData,
      options: DoughnutOptions
    });
  }

  if ($("#chartjs-pie-chart").length) {
    var PieData = {
      datasets: [{
        data: [15, 35, 25, 10, 25],
        backgroundColor: chartColors,
        borderColor: chartColors,
        borderWidth: chartColors
      }],

      // These labels appear in the legend and in the tooltips when hovering different arcs
      labels: [
        'Arquitectura',
        'Ingenieria',
        'Medicina',
        'Derecho',
        'Ad Empresas',
      ]
    };
    var PieOptions = {
      responsive: true,
      animation: {
        animateScale: true,
        animateRotate: true
      }
    };
    var pieChartCanvas = $("#chartjs-pie-chart").get(0).getContext("2d");
    var pieChart = new Chart(pieChartCanvas, {
      type: 'pie',
      data: PieData,
      options: PieOptions
    });
  }


    if ($("#chartjs-pie-chart2").length) {
        var PieData = {
            datasets: [{
                data: [80, 20],
                backgroundColor: chartColors,
                borderColor: chartColors,
                borderWidth: chartColors
            }],

            // These labels appear in the legend and in the tooltips when hovering different arcs
            labels: [
                'Masculino',
                'Femenino',
                
            ]
        };
        var PieOptions = {
            responsive: true,
            animation: {
                animateScale: true,
                animateRotate: true
            }
        };
        var pieChartCanvas = $("#chartjs-pie-chart2").get(0).getContext("2d");
        var pieChart = new Chart(pieChartCanvas, {
            type: 'pie',
            data: PieData,
            options: PieOptions
        });
    }


});