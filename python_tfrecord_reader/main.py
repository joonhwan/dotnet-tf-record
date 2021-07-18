#%%
import tensorflow as tf
import numpy as np


# The following functions can be used to convert a value to a type compatible
# with tf.train.Example.
def _bytes_feature(value):
  """Returns a bytes_list from a string / byte."""
  if isinstance(value, type(tf.constant(0))):
    value = value.numpy() # BytesList won't unpack a string from an EagerTensor.
  return tf.train.Feature(bytes_list=tf.train.BytesList(value=[value]))

def _float_feature(value):
  """Returns a float_list from a float / double."""
  return tf.train.Feature(float_list=tf.train.FloatList(value=[value]))

def _int64_feature(value):
  """Returns an int64_list from a bool / enum / int / uint."""
  return tf.train.Feature(int64_list=tf.train.Int64List(value=[value]))
# %%


print(_bytes_feature(b'test_string'))
print(_bytes_feature(u'test_string'.encode('utf-8')))

print(_float_feature(np.exp(1)))

print(_int64_feature(True))
print(_int64_feature(1))

#%%

feature = _bytes_feature(b'abcdefg123456')
print(feature)
r = feature.SerializeToString()
print('r =', r)

#%%
tf.config.run_functions_eagerly(True)
#%%
ds = tf.data.TFRecordDataset(
    '/Users/vine/prj/tensorflow/dotnet-tf-record/TFRecordNetSamples/data/demo.tfrecord'
)
# for item in ds:
#     print(repr(item))
# Create a description of the features.
feature_description = {
    'feature1': tf.io.VarLenFeature(tf.float32),
    'feature2': tf.io.FixedLenFeature([], tf.string),
    'feature3': tf.io.FixedLenFeature([], tf.string),
    'feature4': tf.io.VarLenFeature( tf.int64)
}


def _parse_function(example_proto):
  # Parse the input `tf.train.Example` proto using the dictionary above.
  return tf.io.parse_single_example(example_proto, feature_description)

parsed_ds = ds.map(_parse_function)
print(parsed_ds)

image_feature = None
for record in parsed_ds:
  print(repr(record))
  print('feature1 => ', record['feature1'])
  print('feature2 => ', record['feature2'])
  image_feature = record['feature2']
  print('feature3 => ', record['feature3'])
  print('feature4 => ', record['feature4'])
      
#%%
# tf.io.write_file('image_feature.png', image_feature)
img = tf.spar

