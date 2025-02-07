#This program takes a CSV from your amazon account to analyze spending habits

import pandas as pd
import numpy as np
import matplotlib.pyplot as plt

# Load the CSV file (update filename as needed)
def load_data(file_path):
    try:
        df = pd.read_csv(file_path)
        return df
    except Exception as e:
        print(f"Error loading file: {e}")
        return None

# Clean and preprocess data
def preprocess_data(df):
    # Convert order date to datetime format
    df['Order Date'] = pd.to_datetime(df['Order Date'], errors='coerce')
    # Convert price column to numeric
    df['Total Cost'] = df['Total Cost'].replace('[\$,]', '', regex=True).astype(float)
    return df

# Analyze spending trends
def analyze_spending(df):
    df['Month'] = df['Order Date'].dt.to_period('M')
    monthly_spending = df.groupby('Month')['Total Cost'].sum()
    
    print("Average Monthly Spending:", np.mean(monthly_spending))
    print("Highest Monthly Spending:", monthly_spending.max())
    print("Lowest Monthly Spending:", monthly_spending.min())
    
    return monthly_spending

#Create a summary stats table
def summary_statistics(df)
    stats = df[['Order Date', 'Item Name', 'Total Cost']].copy()
    stats_summary = {
        'Min': df['Total Cost'].min(),
        'Max': df['Total Cost'].max(),
        'Median': df['Total Cost'].median(),
        'Mean': df['Total Cost'].mean(),
        'Mode': df['Total Cost'].mode()[0] if not df['Total Cost'].mode().empty else np.nan,
        'Std Dev': df['Total Cost'].std()
    }
    
    print("\nSpending Statistics:")
    print(pd.DataFrame([stats_summary]))
    return stats

# Visualize spending trends
def plot_spending(monthly_spending):
    plt.figure(figsize=(10, 5))
    monthly_spending.plot(kind='line', marker='o', linestyle='-', color='b')
    plt.title('Amazon Monthly Spending Trend')
    plt.xlabel('Month')
    plt.ylabel('Total Spending ($)')
    plt.grid(True)
    plt.show()

# Main function
def main():
    file_path = 'amazon_purchase_history.csv'  # Update with your actual CSV file path
    df = load_data(file_path)
    if df is not None:
        df = preprocess_data(df)
        monthly_spending = analyze_spending(df)
        plot_spending(monthly_spending)

if __name__ == "__main__":
    main()
