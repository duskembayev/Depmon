var source = require('vinyl-source-stream');
var gulp = require('gulp');
var browserify = require('browserify');

gulp.task('js', function () {
    var bundle = browserify({
        entries: ['./app/main.jsx'],
        extensions: ['.jsx', '.js', '.json']
    });
    bundle.bundle()
        .on('error', function (err) { console.log(err.message) })
        .pipe(source('bundle.js'))
        .pipe(gulp.dest('./Scripts'));
});

gulp.task('default', ['js'])