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


ds = tf.data.TFRecordDataset(tfrecord_path)
ds = ds.map(_parse_function)

for example in ds:
    name = example['name'].numpy() # bytes
    imageFile = example['imageFile'].numpy()  # bytes
    label = example['label'].numpy() # bytes 
    # print(type(name), type(imageFile), type(label))
    print(f'{name} : {len(imageFile)} bytes, label={label}')