var source = require('vinyl-source-stream');
var gulp = require('gulp');
var stylus = require('gulp-stylus');
var rename = require('gulp-rename');
var browserify = require('browserify');
var path = require('path');

gulp.task('js', function () {
    var bundle = browserify({
        entries: ['./App/main.jsx'],
        extensions: ['.jsx', '.js', '.json']
    });
    bundle.bundle()
        .on('error', function (err) { console.log(err.message) })
        .pipe(source('scripts.js'))
        .pipe(gulp.dest('./Public'));
});

gulp.task('css', function () {
    gulp.src('./Content/main.styl')
      .pipe(stylus({
          'paths': [path.join(__dirname, '/Content')],
          'include css': true
        }))
      .pipe(rename('styles.css'))
      .pipe(gulp.dest('./Public'));
});

gulp.task('default', ['js', 'css'])