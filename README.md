# SampleInteropApp

## 概要
このプロジェクトは、WPFアプリケーションとコンソールアプリケーション間でIPC（プロセス間通信）を使用したToDoリスト管理アプリケーションです。

## プロジェクト構成
- **SimpleToDoApp.Core**: 共通のビジネスロジックとデータモデル
- **SimpleToDoApp.Wpf**: WPFベースのGUIアプリケーション
- **SimpleToDoApp.ConsoleApp**: コンソールベースのアプリケーション
- **SimpleToDoApp.Wpf.IpcServer**: WPFアプリケーション用のIPCサーバー

## 機能
- ToDoタスクの作成、編集、削除
- WPFアプリケーションとコンソールアプリケーション間のリアルタイム同期
- IPCを使用したプロセス間通信
- タスクの優先度管理
- タスクの完了状態の管理

## 必要条件
- .NET 6.0 以上
- Visual Studio 2022 または Visual Studio Code
- Windows OS（WPFアプリケーションのため）

## セットアップ方法
1. リポジトリをクローンします：
```bash
git clone https://github.com/yourusername/SampleInteropApp.git
```

2. プロジェクトディレクトリに移動します：
```bash
cd SampleInteropApp
```

3. 依存関係を復元します：
```bash
dotnet restore
```

## ビルドと実行
### WPFアプリケーション
```bash
cd SimpleToDoApp.Wpf
dotnet run
```

### コンソールアプリケーション
```bash
cd SimpleToDoApp.ConsoleApp
dotnet run
```

## アーキテクチャ
このアプリケーションは以下のアーキテクチャパターンを使用しています：
- MVVMパターン（WPFアプリケーション）
- クリーンアーキテクチャ（Coreプロジェクト）
- IPC（プロセス間通信）によるリアルタイム同期