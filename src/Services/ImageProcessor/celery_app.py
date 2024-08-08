from celery import Celery
import celery_config

app = Celery('myapp')

app.config_from_object(celery_config)

app.autodiscover_tasks(['tasks'])

''' task_queues = {
        'artist_image_uploaded': {
            'exchange': 'artist_image_uploaded',
            'routing_key': 'artist_image_uploaded'
        }
    },
    task_routes = {
        'ImageProcessor.tasks.process_artist_image': {
            'exchange': 'artist_image_uploaded',
            'routing_key': 'artist_image_uploaded',
            'queue': 'artist_image_uploaded'
        }
    } '''

'''
app.conf.task_queues = {
    'default': {
        'exchange': 'default',
        'binding_key': 'default',
    },
    'artist_image_uploaded': {
        'exchange': 'artist_image_uploaded',
        'binding_key': 'artist_image_uploaded',
    }
}

app.conf.task_routes = {
    'process_artist_image': {'queue': 'artist_image_uploaded'}
}
'''