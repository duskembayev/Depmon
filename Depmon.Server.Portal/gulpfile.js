var source = require('vinyl-source-stream');
var gulp = require('gulp');
var browserify = require('browserify');

gulp.task('js', function () {
    var bundle = browserify({
        entries: ['./App/main.js'],
        extensions: ['.jsx', '.js', '.json']
    });
    bundle.bundle()
        .on('error', function (err) { console.log(err.message) })
        .pipe(source('scripts.js'))
        .pipe(gulp.dest('./Public'));
});

gulp.task('default', ['js']);