// Verilen metni satır satır böler
var lines = document.getElementsByTagName('pre')[0].outerText.split('\r\n');

// Sonuçları depolamak için boş bir dizi oluşturur
var movies = [];

// Satırları işler
for (var i = 0; i < lines.length; i++) {
    // "Movie Name" veya "Image URL" içeren satırları işler
    if (lines[i].startsWith('Movie Name: ')) {
        var movieName = lines[i].replace('Movie Name: ', '');
        var imageUrl = lines[i + 1].replace('Image URL: ', '');

        // Movie Name ve Image URL'i bir nesne olarak diziye ekler
        movies.push({
            "Movie Name": movieName,
            "Image URL": imageUrl
        });
        i++; // İki satır işlendiği için indeksi artır
    }
}

// Verileri HTML yapısına ekler
document.addEventListener('DOMContentLoaded', function () {
    var testimonialsCarousel = document.querySelector('.tm-testimonials-carousel');

    for (var i = 0; i < movies.length; i++) {
        var movie = movies[i];
        var figure = document.createElement("figure");
        figure.className = "tm-testimonial-item";

        var img = document.createElement("img");
        img.src = movie["Image URL"];
        img.alt = movie["Movie Name"];
        img.className = "img-fluid mx-auto";

        var blockQuote = document.createElement("blockquote");
        blockQuote.textContent = movie["Movie Name"];

        var figCaption = document.createElement("figcaption");

        figure.appendChild(img);
        figure.appendChild(blockQuote);
        figure.appendChild(figCaption);

        testimonialsCarousel.appendChild(figure);
    }
});
