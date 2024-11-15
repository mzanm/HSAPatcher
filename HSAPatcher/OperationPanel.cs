using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Windows.Forms.Automation;
namespace HSAPatcher;
public class OperationPanel : FlowLayoutPanel
{
    private ProgressBar progressBar;
    private Label label;
    private ListBox listBox;
    private List<string> historyItems;

    public OperationPanel()
    {
        historyItems = new List<string>();

        this.FlowDirection = FlowDirection.TopDown;
        this.AutoSize = true;
        this.WrapContents = false;

        progressBar = new ProgressBar
        {
            Width = 50,
            Height = 20
        };

        label = new Label
        {
            Text = "Operation Status",
            AutoSize = true
        };

        listBox = new ListBox
        {
            Width = 200,
            Height = 100
        };

        listBox.DataSource = historyItems;

        this.Controls.Add(progressBar);
        this.Controls.Add(label);
        this.Controls.Add(listBox);

        UpdateVisibility();
    }

    public void AddHistoryItem(string item)
    {
        historyItems.Add(item);
        listBox.DataSource = null;
        listBox.DataSource = historyItems;
        UpdateVisibility();
    }

    public void ClearHistory()
    {
        historyItems.Clear();
        listBox.DataSource = null;
        listBox.DataSource = historyItems;
        UpdateVisibility();
    }

    public ProgressBar ProgressBar
    {
        get { return progressBar; }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string LabelText
    {
        get { return label.Text; }
        set
        {
            if (label.Text != value)
            {
                label.Text = value;
                label.AccessibilityObject.RaiseAutomationNotification(AutomationNotificationKind.Other, AutomationNotificationProcessing.CurrentThenMostRecent, value);
                AddHistoryItem(value);
            }
        }
    }

    public List<string> HistoryItems
    {
        get { return historyItems; }
    }

    public void UpdateProgress(int progressValue, string? text = null)
    {
        if (text != null)
        {
            LabelText = text;
        }
        if (progressValue != progressBar.Value)
        {
            progressBar.Value = progressValue;
        }

    }

    private void UpdateVisibility()
    {
        bool shouldBeVisible = historyItems.Count > 0;
        if (this.Visible != shouldBeVisible)
        {
            this.Visible = shouldBeVisible;
        }
    }
}
