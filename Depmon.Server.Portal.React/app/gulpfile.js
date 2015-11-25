var
  VENDOR,
  compileCss,
  compileAppJs,
  compileVendorJs,
  _ = require('lodash'),
  pkg = require('./package.json'),
  path = require('path'),
  gulp = require('gulp'),
  stylus = require('gulp-stylus'),
  minifyCSS = require('gulp-minify-css'),
  uglify = require('gulp-uglify'),
  streamify = require('gulp-streamify'),
  nib = require('nib'),
  rename = require('gulp-rename'),
  browserify = require('browserify'),
  source = require('vinyl-source-stream'),


VENDOR = [
  'backbone-events-standalone',
  'classnames',
  'page',
  'lodash',
  'react',
  'react-dom',
];

compileVendorJs = function (opts) {
    opts = _.assign({

    }, opts);

    var bundle = browserify();

    _.forEach(VENDOR, function (vendor) {
        if (!_.startsWith(vendor, '!')) {
            bundle.require(vendor, { expose: vendor });
        }
    });

    bundle.bundle()
      .on('error', function (err) { console.log(err.message) })
      .pipe(source('vendor.js'))
      .pipe(streamify(uglify()))
      .pipe(gulp.dest('./../public/assets'));
};

compileAppJs = function (opts) {
    opts = _.assign({
        minify: false
    }, opts);

    var bundle = browserify({
        entries: ['./index.js'],
        paths: ['./node_modules']
    });

    _.forEach(VENDOR, function (lib) {
        bundle.exclude(_.startsWith(lib, '!') ? lib.substr(1) : lib);
    });

    bundle = bundle
      .bundle()
      .on('error', function (err) { console.log(err.message) })
      .pipe(source('script.js'));

    if (opts.minify) bundle = bundle.pipe(streamify(uglify()));

    bundle.pipe(gulp.dest('./../public/assets'));
};

compileCss = function (opts) {
    opts = _.assign({
        minify: false
    }, opts);

    var task = gulp.src('./stylesheets/index.styl')
    .pipe(stylus({
        'paths': [path.join(__dirname, '/node_modules')],
        'include css': true,
        'use': [nib()],
        'urlfunc': 'embedurl',
        'linenos': true,
        'define': {
            '$version': pkg.version
        }
    }));
    task = task.pipe(rename('style.css'));
    if (opts.minify) task = task.pipe(minifyCSS());
    task = task.pipe(gulp.dest('./../public/assets/'));
};

// ===============================================================
// DEVELOPMENT
// ===============================================================

gulp.task('js:app', function () {
    compileAppJs();
});

gulp.task('js:vendor', function () {
    compileVendorJs();
});

gulp.task('js', ['js:app', 'js:vendor']);

gulp.task('css', function () {
    compileCss();
});

gulp.task('assets', ['js', 'css']);

gulp.task('assets:min', ['js:min', 'css:min']);
