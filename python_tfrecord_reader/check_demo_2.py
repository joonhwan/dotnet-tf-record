#%%
import tensorflow as tf
import numpy as np
import os
from pathlib import Path
#%%
cwd = os.getcwd()
data_dir = Path(cwd).parent
tfrecord_path = data_dir / 'data/marble.tfrecord'
print(tfrecord_path)
#%%
feature_description = {
    'name': tf.io.FixedLenFeature([], tf.string),
    'imageFile': tf.io.FixedLenFeature([], tf.string),
    'label': tf.io.FixedLenFeature([], tf.string),
}

def _parse_function(example_proto):
  # Parse the input `tf.train.Example` proto using the dictionary above.
  return tf.io.parse_single_example(example_proto, feature_description)


ds = tf.data.TFRecordDataset(tfrecord_path)#, compression_type="GZIP")
ds = ds.map(_parse_function)

read_check_dir = Path("./.datasets/marble")
for example in ds.take(5):
    name = example['name'].numpy() # --> bytes
    name = name.decode('utf-8') #  --> string(utf-8)
    name = Path(name).name
    imageFile = example['imageFile']  # Tensor
    imageFileBytes = imageFile.numpy()
    label = example['label'].numpy() # bytes 
    label = label.decode('utf-8') # -> string(utf-8)
    # print(type(name), type(imageFile), type(label))
    print(f'{name} : {len(imageFileBytes)} bytes, label={label}')
    imageOutPath = str(read_check_dir / label / name)
    print(imageOutPath)
    tf.io.write_file(imageOutPath, imageFile)


#%%
tfrecord_path = data_dir / 'data/marble.tfrecord.gz'
ds = tf.data.TFRecordDataset(tfrecord_path, compression_type='GZIP')
ds = ds.map(_parse_function)
for example in ds.take(5):
  name = example['name']
  imageFile = example['imageFile']
  label = example['label']

  print(f'{name} : {len(imageFileBytes)} bytes, label={label}')
  img = tf.io.decode_jpeg(imageFile)
  print(img)
