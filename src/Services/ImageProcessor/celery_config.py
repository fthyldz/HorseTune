from kombu import Queue, Exchange

broker_connection_retry_on_startup = True

task_serializer = 'json'

result_serializer = 'json'

accept_content = ['json', 'application/vnd.masstransit+json']

task_queues = [
    Queue('artist_image_uploaded', Exchange('artist_image_uploaded'), routing_key='artist_image_uploaded')
]

task_routes = {
    'myapp.tasks.process_artist_image': {'queue': 'artist_image_uploaded'}
}

broker_url = 'amqp://guest:guest@localhost:5672//'