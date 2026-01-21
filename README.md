# Unity3D Function Graph Plotter

A lightweight solution for plotting function graphs in Unity3D using UI components.

![Graph Example](https://sun9-79.userapi.com/impg/noM0EKqMZl4mGwDDo1DonS4dz6VG2e4GzdyiAA/i6oikJmQw-0.jpg?size=425x355&quality=95&sign=bb6be969bbe9d69c6c22960effd533f9&type=album)

## Features

- Dynamic point plotting on a 2D graph
- Configurable axis dimensions and step counts
- Support for negative axis values
- TextMeshPro integration for axis labels
- Grid lines for visual reference
- Easy integration via prefab

## Requirements

- Unity 2020.3 LTS or newer
- TextMeshPro package
- UnityEngine.UI.Extensions (included)

## Installation

1. Download the latest release
2. Import the package into your Unity project
3. Drag `GraphController.prefab` from `Assets/theGrahCreator/` into your scene

## Configuration

| Parameter | Description |
|-----------|-------------|
| Axis Length X/Y | Total length of each axis in units |
| Steps X/Y | Number of divisions on each axis |
| Origin X/Y | Starting coordinate offset |
| Point Prefab | Visual representation of plotted points |
| Line Renderer | UILineRenderer for connecting points |

## Usage

### Basic Point Plotting

```csharp
public GraphCreator graph;

void PlotFunction()
{
    for (float x = 0; x <= 10; x += 0.5f)
    {
        graph.InputX = x;
        graph.InputY = Mathf.Sin(x) * 5;
        graph.AddPoint();
    }
}
```

### Input Controller

The `GraphCreatorInputController` component provides UI bindings:
- `AddPoint()` — plots a point from input fields
- `ClearGraph()` — removes all points
- `DeleteLastPoint()` — removes the most recent point

## Project Structure

```
Assets/theGrahCreator/
├── Scripts/
│   ├── MathPart/
│   │   └── GraphCreator.cs         # Core graph logic
│   ├── GraphCreatorInputController.cs
│   └── CustomParser.cs             # Number parsing utility
├── Scenes/
│   └── SampleScene.unity
├── unity-ui-extensions/            # UI Extensions library
└── GraphController.prefab
```

## Screenshots

**Standard Configuration:**

![Standard Graph](https://sun9-21.userapi.com/impg/MC3H-ck0iU0yMFvP7UYqU_Us5iPPct5JieM3PA/yz2sFiXfcfQ.jpg?size=542x607&quality=95&sign=2cecabd7b360bb57aab2290cd3b2972a&type=album)

**Negative Axis Values:**

![Negative Graph](https://sun9-21.userapi.com/impg/Gq3ukiATLHq9FUlQKiFi_AjsxDobAoGQzNVPHg/IjG_m2Qci7o.jpg?size=486x583&quality=95&sign=919f4883d163c5a5e011e5dee4104461&type=album)

## API Reference

### GraphCreator

| Property | Type | Description |
|----------|------|-------------|
| `DataPoints` | `List<Vector2>` | All plotted coordinates |
| `PointInstances` | `List<GameObject>` | Instantiated point objects |
| `InputX` | `float` | X coordinate for next point |
| `InputY` | `float` | Y coordinate for next point |

| Method | Description |
|--------|-------------|
| `AddPoint()` | Plots current InputX/InputY |
| `DeletePoint()` | Removes last plotted point |
| `DeleteAllPoints()` | Clears the entire graph |
| `InitializeGraph()` | Rebuilds axis labels |

### CustomParser

| Method | Description |
|--------|-------------|
| `ParseToDouble(string)` | Culture-aware string to double conversion |

## License

MIT License — see [LICENSE.md](LICENSE.md)

---

# Построение графиков функций в Unity3D

Легковесное решение для построения графиков функций в Unity3D с использованием UI компонентов.

## Возможности

- Динамическое построение точек на 2D графике
- Настраиваемые размеры осей и количество делений
- Поддержка отрицательных значений осей
- Интеграция с TextMeshPro
- Линии сетки
- Простая интеграция через prefab

## Установка

1. Скачайте последний релиз
2. Импортируйте пакет в проект Unity
3. Перетащите `GraphController.prefab` из `Assets/theGrahCreator/` в сцену

## Лицензия

MIT License — см. [LICENSE.md](LICENSE.md)
