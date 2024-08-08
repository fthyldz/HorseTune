import uuid
from celery_app import app
from PIL import Image

class ArtistImageUploadedEvent:
    def __init__(self, id: uuid.UUID, original_path: str):
        self.id = id
        self.original_path = original_path
    
@app.task
def process_artist_image(event_data: ArtistImageUploadedEvent):
    print("# Görüntü işleme işlemleri")
    return "Görüntü işleme işlemleri başarılı"

'''class BaseEvent:
    def __init__(self):
        pass

class IntegrationEvent(BaseEvent):
    def __init__(self, id: uuid.UUID):
        self.id = id

class ArtistImageUploadedEvent(IntegrationEvent):
    def __init__(self, original_path: str, id: uuid.UUID):
        super().__init__(id=id)
        self.original_path = original_path'''
    
'''@app.task(name='tasks.process_image')
def process_image(image_path):
    try:
        # Resmi aç
        with Image.open(image_path) as img:
            
            print(f"Processing image: {image_path}")
            # Resmi işle (örnek: boyutlandırma)
            #img.thumbnail((300, 300))
            
            # İşlenmiş resmi kaydet
            #filename, ext = os.path.splitext(image_path)
            #processed_path = f"{filename}_processed{ext}"
            #img.save(processed_path)
        
        return {
            "success": True,
            "processed_image_path": image_path
        }
    except Exception as e:
        return {
            "success": False,
            "error": str(e)
        }'''