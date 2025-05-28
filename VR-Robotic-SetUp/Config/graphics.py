import matplotlib.pyplot as plt
import pandas as pd

# Datos de la distancia media
data_distance = {
    'Conf': ['Conf 1', 'Conf 2', 'Conf 3', 'Conf 4', 'Conf 5', 'Conf 6', 'Conf 7', 'Conf 8', 'Conf 9', 'Conf 10'],
    'Value': [0.66735, 0.7441, 0.74, 0.7645, 0.7572, 0.76, 0.7616, 0.775, 0.80625, 0.81005]
}

# Datos de los pasos promedio
data_steps = {
    'Conf': ['Conf 1', 'Conf 2', 'Conf 3', 'Conf 4', 'Conf 5', 'Conf 6', 'Conf 7', 'Conf 8', 'Conf 9', 'Conf 10'],
    'Value': [130.715, 127.91, 127.625, 128.43, 127.2, 127.72, 123.2, 125, 124.175, 119.925]
}


# Crear DataFrames
df_distance = pd.DataFrame(data_distance)
df_steps = pd.DataFrame(data_steps)

# Invertir los valores de la distancia media pero mantener las etiquetas en el orden original
df_distance['Value'] = df_distance['Value'][::-1].reset_index(drop=True)
df_distance['Comfort'] = df_distance['Conf'].str.replace('Conf', 'Comf')

# Actualizar las etiquetas de pasos promedio
df_steps['Comfort'] = df_steps['Conf'].str.replace('Conf', 'Comf')

# Graficar la distancia media por nivel de confort
plt.figure(figsize=(10, 6))
plt.plot(df_distance['Comfort'], df_distance['Value'], marker='o', color='orange')
plt.title('Average Distance by Comfort Level')
plt.xlabel('Comfort Level')
plt.ylabel('Average Distance')
plt.grid(True)
plt.show()

# Graficar los pasos promedio por nivel de confort
plt.figure(figsize=(10, 6))
plt.plot(df_steps['Comfort'], df_steps['Value'], marker='o', color='orange')
plt.title('Average Steps by Comfort Level')
plt.xlabel('Comfort Level')
plt.ylabel('Average Steps')
plt.grid(True)
plt.show()
