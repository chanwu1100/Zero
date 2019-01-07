﻿using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Zero.Edit
{
    public class PackingTagEditorWin : AEditorWin
    {
        /// <summary>
        /// 打开窗口
        /// </summary>
        public static void Open()
        {
            var win = EditorWindow.GetWindow<PackingTagEditorWin>();
            win.titleContent = new GUIContent("Packing Tag Manager");
            win.minSize = new Vector2(800, 500);
            //win.maxSize = new Vector2(1000, 500);
            win.Show();
        }

        Dictionary<string, List<TextureImporter>> _ptData;

        HashSet<string> _selectKey = new HashSet<string>();

        Vector2 _pos = Vector2.zero;

        private void OnGUI()
        {
            GUILayout.BeginVertical();

            if(GUILayout.Button("开始扫描!"))
            {
                _ptData = new FindAllPackingTagCommand().Excute();
            }

            _pos = GUILayout.BeginScrollView(_pos, GUILayout.Height(400));
            foreach(var key in _ptData.Keys)
            {
                if(GUILayout.Toggle(_selectKey.Contains(key), key))
                {
                    _selectKey.Add(key);
                }
                else
                {
                    _selectKey.Remove(key);
                }
            }
            GUILayout.EndScrollView();

            if(GUILayout.Button("删除选中的Packing Tag"))
            {
                DeleteSelected();
            }

            GUILayout.EndVertical();
        }

        public void DeleteSelected()
        {
            foreach(var key in _selectKey)
            {
                List<TextureImporter> tiList;
                _ptData.TryGetValue(key, out tiList);
                foreach(var ti in tiList)
                {
                    ti.spritePackingTag = string.Empty;
                }
            }

            _ptData = new FindAllPackingTagCommand().Excute();
        }        
    }
}