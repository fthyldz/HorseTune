from celery_app import app

if __name__ == '__main__':
    argv = [
        'worker',
        '--loglevel=INFO',
        '-E',
        '-Q',
        'artist_image_uploaded'
    ]
    app.worker_main(argv)